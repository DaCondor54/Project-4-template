import { queryClient } from "@/lib/queryClient";
import axios from "axios";

export const http = axios.create({
    baseURL: 'http://localhost:3000'
})

// TODO: improve this
http.interceptors.response.use(
    (response) => response,
    (error) => {
        if(error.response?.status === 401) {
            queryClient.clear()
        }
        return Promise.reject(error)
    }
)