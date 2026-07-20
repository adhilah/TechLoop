import axios from "axios";
import type { Technology } from "../types/technology";

const api = axios.create({
    baseURL: "http://localhost:5264",
});

class TechnologyService {

    async getAll(): Promise<Technology[]> {

        const response = await api.get<Technology[]>(
            "/technologies"
        );

        return response.data;

    }

    async getById(id: number): Promise<Technology> {

        const response = await api.get<Technology>(
            `/technologies/${id}`
        );

        return response.data;

    }

}

export default new TechnologyService();