import { http } from "@/services/http";
import { useQuery } from "@tanstack/react-query";

interface Flem {
    flemRate: number;
}

export const useFlems = (key: string) => {
    return useQuery({
        queryKey: ['flems', key],
        queryFn: () => http.get<Flem[]>('/flem'),
        select: (data) => data.data
    })
}

export const useFlem = () => {
    return useQuery({
        queryKey: ['flem'],
        queryFn: () => http.post<Flem>('/flem'),
        select: (data) => data.data.flemRate,
        enabled: false,
    })
}

export const useAverageFlem = (key: string) => {
    return useQuery({
        queryKey: ['averageflem', key],
        queryFn: () => http.get<Flem>('/averageflem'),
        select: (data) => data.data.flemRate
    })
}