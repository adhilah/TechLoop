import { Routes, Route } from "react-router-dom";
import LearnerLayout from "../components/layout/learner/LearnerLayout";
import LandingPage from "../pages/landing/LandingPage";
import Learn from "../pages/learning/Learn";
import ProtectedRoute from "./ProtectedRoute";

export default function LearnerRoutes() {
    return (
        <Routes>
            <Route element={<ProtectedRoute />}>
                <Route element={<LearnerLayout />}>
                    <Route index element={<LandingPage />} />
                    <Route path="learn" element={<Learn />} />
                </Route>
            </Route>
        </Routes>
    );
}