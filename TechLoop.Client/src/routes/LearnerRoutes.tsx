import { Routes, Route } from "react-router-dom";
import LearnerLayout from "../components/layout/learner/LearnerLayout";
import LandingPage from "../pages/landing/LandingPage";
import Learn from "../pages/learning/Learn.tsx";

export default function LearnerRoutes() {
    return (
        <Routes>
            <Route element={<LearnerLayout />}>
                <Route index element={<LandingPage />} />
                <Route path="learn" element={<Learn />} />
            </Route>
        </Routes>
    );
}