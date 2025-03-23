// apiService.ts
import axios, { AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from 'axios';

// יצירת אינסטנציה של Axios
const apiClient = axios.create({
    baseURL: 'https://localhost:7253/api',
});

// אינטרספטור לבקשות
apiClient.interceptors.request.use(
    (config: InternalAxiosRequestConfig) => {
        const token = sessionStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`; // הגדרת הכותרת
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// אינטרספטור לתגובות
apiClient.interceptors.response.use(
    (response: AxiosResponse) => {
        return response;
    },
    (error) => {
        console.error('API Error:', error);
        return Promise.reject(error);
    }
);

export default apiClient;