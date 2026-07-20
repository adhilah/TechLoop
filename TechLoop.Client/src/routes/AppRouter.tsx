import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "../pages/auth/LoginPage";
import RegisterPage from "../pages/auth/RegisterPage";
import LearnerRoutes from "./LearnerRoutes.tsx";

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                //<Route path="/" element={<Navigate to="/login" replace />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/learner/*" element={<LearnerRoutes />} />
            </Routes>
        </BrowserRouter>
    );
}