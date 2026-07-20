import React from "react";
import Hero from "../../components/landing/Hero";
import Metrics from "../../components/landing/Metrics";
import Features from "../../components/landing/Features";

const LandingPage: React.FC = () => {
    return (
        <div className="bg-bg min-h-screen font-sans">
            <Hero />
            <Metrics />
            <Features />
        </div>
    );
};

export default LandingPage;