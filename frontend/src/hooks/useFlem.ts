import { http } from "@/services/http";
import { useQuery } from "@tanstack/react-query";

interface Flem {
    reason: string;
}

export const useFlem = () => {
    return useQuery({
        queryKey: ['flem'],
        queryFn: () => http.get<Flem>('/flem'),
        select: (data) => data.data.reason
    })
}