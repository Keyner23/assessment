class Api {
    constructor() {
        this.baseUrl = 'http://localhost:5261/api/v1';
    }

    getToken() {
        return localStorage.getItem('jwt_token');
    }

    setToken(token) {
        localStorage.setItem('jwt_token', token);
    }

    logout() {
        localStorage.removeItem('jwt_token');
        window.location.href = 'index.html';
    }

    async request(endpoint, method = 'GET', body = null) {
        const headers = {
            'Content-Type': 'application/json'
        };

        const token = this.getToken();
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        const config = {
            method,
            headers
        };

        if (body) {
            config.body = JSON.stringify(body);
        }

        try {
            const response = await fetch(`${this.baseUrl}${endpoint}`, config);

            if (response.status === 401) {
                // Unauthorized, redirect to login if not already there
                if (!window.location.href.includes('index.html')) {
                    this.logout();
                }
                throw new Error('Unauthorized');
            }

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || `Error ${response.status}`);
            }

            // Some endpoints might return empty body (like 200 OK with no content)
            const text = await response.text();
            return text ? JSON.parse(text) : {};

        } catch (error) {
            console.error('API Request Failed:', error);
            throw error;
        }
    }

    async login(username, password) {
        // The API returns void/200 OK on login? 
        // Wait, looking at the swagger definition:
        // /api/v1/Auth/login returns 200 OK. 
        // Usually login returns a token. 
        // Let's assume the token is in the response body even if swagger says "200 OK". 
        // If not, we might have an issue. 
        // Re-reading swagger: It says "responses: 200: description: OK". 
        // It DOESNT specify the return schema for 200. 
        // I will assume it returns a JWT string or an object with { token: "..." }.
        // I'll log the response to verify during testing.
        return this.request('/Auth/login', 'POST', { username, password });
    }

    async getCourses() {
        return this.request('/Course', 'GET');
    }

    async createCourse(title, status = 1) {
        // Status: 0 or 1. Assuming 1 is active.
        return this.request('/Course', 'POST', { title, status });
    }

    async getLessons(courseId) {
        // The swagger has two endpoints for getting lessons?
        // /api/v1/Lesson/{courseId} -> GET. This seems to be "Get Lessons by Course Id".
        // /api/v1/Lesson/lesson/{id} -> GET. This seems to be "Get Lesson by Lesson Id".
        return this.request(`/Lesson/${courseId}`, 'GET');
    }

    async createLesson(courseId, title, order) {
        return this.request('/Lesson', 'POST', { courseId, title, order });
    }
}

const api = new Api();
