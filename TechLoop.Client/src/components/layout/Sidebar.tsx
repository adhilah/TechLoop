import React, { useState } from "react";
import {LayoutDashboard, BookOpen, Dumbbell, Code2, Users, MessagesSquare, Bot, BarChart3, Trophy, Bell, User, Settings, ShieldCheck} from "lucide-react";

type NavItem = {
    label: string;
    icon: React.ComponentType<{ size?: number; className?: string }>;
    badge?: number;
    dot?: boolean;
};

type NavSection = {
    title: string;
    items: NavItem[];
};

const sections: NavSection[] = [
    {
        title: "Main",
        items: [
            { label: "Dashboard", icon: LayoutDashboard },
            { label: "Learn", icon: BookOpen },
            { label: "Practice", icon: Dumbbell },
            { label: "Coding", icon: Code2, badge: 12 },
            { label: "Classrooms", icon: Users },
            { label: "Community", icon: MessagesSquare },
            { label: "Messages", icon: MessagesSquare, dot: true },
            { label: "AI Mentor", icon: Bot },
        ],
    },
    {
        title: "Insights",
        items: [
            { label: "Analytics", icon: BarChart3 },
            { label: "Leaderboard", icon: Trophy },
            { label: "Notifications", icon: Bell },
        ],
    },
    {
        title: "Account",
        items: [
            { label: "Profile", icon: User },
            { label: "Settings", icon: Settings },
            { label: "Admin", icon: ShieldCheck },
        ],
    },
];

export default function Sidebar() {
    const [active, setActive] = useState("Dashboard");

    return (
        <div className="h-screen w-64 bg-[#0a1224] text-slate-300 flex flex-col border-r border-white/5 font-sans">
            {/* Logo */}
            <div className="flex items-center gap-3 px-5 py-5 border-b border-white/5">
                <div className="w-8 h-8 rounded-lg bg-teal-400 flex items-center justify-center text-[#0a1224] font-bold text-sm">
                    TL
                </div>
                <span className="text-white font-semibold text-[15px]">TechLoop</span>
            </div>

            {/* Nav */}
            <nav className="flex-1 overflow-y-auto px-3 py-4 space-y-6">
                {sections.map((section) => (
                    <div key={section.title}>
                        <p className="px-3 mb-2 text-[11px] font-semibold tracking-wider text-slate-500 uppercase">
                            {section.title}
                        </p>
                        <ul className="space-y-1">
                            {section.items.map((item) => {
                                const isActive = active === item.label;
                                const Icon = item.icon;
                                return (
                                    <li key={item.label}>
                                        <button
                                            onClick={() => setActive(item.label)}
                                            className={`w-full flex items-center gap-3 px-3 py-2 rounded-lg text-sm transition-colors ${
                                                isActive
                                                    ? "bg-gradient-to-r from-teal-500/20 to-transparent text-white border-l-2 border-teal-400"
                                                    : "text-slate-400 hover:bg-white/5 hover:text-white border-l-2 border-transparent"
                                            }`}
                                        >
                                            <Icon size={17} className={isActive ? "text-teal-400" : "text-slate-500"} />
                                            <span className="flex-1 text-left font-medium">{item.label}</span>
                                            {item.badge !== undefined && (
                                                <span className="text-[11px] font-semibold bg-teal-500/20 text-teal-300 rounded-full px-2 py-0.5">
                          {item.badge}
                        </span>
                                            )}
                                            {item.dot && (
                                                <span className="w-2 h-2 rounded-full bg-red-500" />
                                            )}
                                        </button>
                                    </li>
                                );
                            })}
                        </ul>
                    </div>
                ))}
            </nav>

            {/* User */}
            <div className="p-3 border-t border-white/5">
                <div className="flex items-center gap-3 px-2 py-2 rounded-lg border border-white/5 bg-white/[0.02]">
                    <div className="w-9 h-9 rounded-full bg-teal-400/20 border border-teal-400/40 flex items-center justify-center text-teal-300 font-semibold text-xs">
                        AM
                    </div>
                    <div className="leading-tight">
                        <p className="text-white text-sm font-medium">Arjun Mehta</p>
                        <p className="text-slate-500 text-xs">Contributor · Lvl 14</p>
                    </div>
                </div>
            </div>
        </div>
    );
}