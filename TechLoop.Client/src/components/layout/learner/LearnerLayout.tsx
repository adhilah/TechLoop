import { Outlet } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import Navbar from "./Navbar";
import Sidebar from "./Sidebar";
import Footer from "./Footer";

function useAutoHideNavbar(scrollRef: React.RefObject<HTMLDivElement | null>) {
    const [hidden, setHidden] = useState(false);
    const lastScrollY = useRef(0);

    useEffect(() => {
        const el = scrollRef.current;
        if (!el) return;

        const THRESHOLD = 6;
        const REVEAL_ZONE = 80;

        const handleScroll = () => {
            const currentY = el.scrollTop;
            const delta = currentY - lastScrollY.current;

            if (currentY < REVEAL_ZONE) {
                setHidden(false);
            } else if (delta > THRESHOLD) {
                setHidden(true);
            } else if (delta < -THRESHOLD) {
                setHidden(false);
            }

            lastScrollY.current = currentY;
        };

        el.addEventListener("scroll", handleScroll, { passive: true });

        return () => el.removeEventListener("scroll", handleScroll);
    }, [scrollRef]);

    return hidden;
}

export default function LearnerLayout() {
    const scrollRef = useRef<HTMLDivElement>(null);
    const navHidden = useAutoHideNavbar(scrollRef);

    return (
        <div className="flex h-screen bg-[#081423]">
            {/* Fixed Sidebar */}
            <Sidebar />

            {/* Right Content */}
            <div className="ml-64 flex min-h-screen flex-1 flex-col">
                <Navbar hidden={navHidden} />

                <main ref={scrollRef} className="flex-1 overflow-y-auto overflow-x-hidden bg-[#081423] pt-20">
                    {/* Center Content */}
                    <div className="space-y-8">
                        <Outlet />
                    </div>

                    <Footer />
                </main>
            </div>
        </div>
    );
}