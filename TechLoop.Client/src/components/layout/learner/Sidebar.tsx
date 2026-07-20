//import React, { useState } from "react";
import {
    LayoutDashboard, BookOpen, Dumbbell, Code2, Users, MessagesSquare,
    Bot, BarChart3, Trophy, Bell, User, Settings, LogOut, LogIn,
} from "lucide-react";
import { NavLink } from "react-router-dom";

type NavItem = {
    label: string;
    path: string;
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
            { label: "Dashboard", path: "/learner/dashboard", icon: LayoutDashboard },
            { label: "Learn", path: "/learner/learn", icon: BookOpen },
            { label: "Practice", path: "/learner/practice", icon: Dumbbell },
            { label: "Coding", path: "/learner/coding", icon: Code2, badge: 12 },
            { label: "Classrooms", path: "/learner/classrooms", icon: Users },
            { label: "Community", path: "/learner/community", icon: MessagesSquare },
            { label: "Messages", path: "/learner/messages", icon: MessagesSquare, dot: true },
            { label: "AI Mentor", path: "/learner/ai-mentor", icon: Bot },
        ],
    },
    {
        title: "Insights",
        items: [
            { label: "Analytics",path:" ", icon: BarChart3 },
            { label: "Leaderboard",path:" ", icon: Trophy },
            { label: "Notifications",path:" ", icon: Bell },
        ],
    },
    {
        title: "Account",
        items: [
            { label: "Profile",path:" ", icon: User },
            { label: "Settings",path:" ", icon: Settings },
        ],
    },
];

interface SidebarProps {
    isAuthenticated?: boolean;
    userName?: string;
    userRole?: string;
    userInitials?: string;
    onLogin?: () => void;
    onLogout?: () => void;
}

export default function Sidebar({
                                    isAuthenticated = true,
                                    userName = "Arjun Mehta",
                                    userRole = "Contributor · Lvl 14",
                                    userInitials = "AM",
                                    onLogin,
                                    onLogout,
                                }: SidebarProps) {


    return (
        <div className="fixed left-0 top-16 h-[calc(100vh-64px)] w-64 bg-[#0A1930] border-r border-white/5 flex flex-col">
            {/* Logo */}
            <div className="flex items-center gap-2.5 px-4 py-4 border-b border-white/[0.06] shrink-0">
                <div className="w-7 h-7 rounded-lg bg-[#17D4C3] flex items-center justify-center text-[#081423] font-bold text-[13px]">
                    TL
                </div>
                <span className="text-white font-semibold text-[14px] tracking-tight">
                    TechLoop
                </span>
            </div>

            {/* Nav */}
            <nav className="flex-1 px-2.5 py-4 space-y-4">
                {sections.map((section) => (
                    <div key={section.title}>
                        <p className="px-2 mb-1.5 text-[10px] font-semibold tracking-[0.08em] text-slate-500 uppercase">
                            {section.title}
                        </p>
                        <ul className="space-y-0.5">
                            {section.items.map((item) => {
                                const Icon = item.icon;

                                return (
                                    <li key={item.label}>
                                        <NavLink
                                            to={item.path}
                                            className={({ isActive }) =>
                                                `relative flex items-center gap-2.5 px-3 py-2 rounded-lg text-[13px] transition-all duration-200 ${
                                                    isActive
                                                        ? "bg-[#17D4C3]/12 text-white"
                                                        : "text-slate-400 hover:bg-white/5 hover:text-white"
                                                }`
                                            }
                                        >
                                            {({ isActive }) => (
                                                <>
                                                    <Icon
                                                        size={16}
                                                        className={
                                                            isActive
                                                                ? "text-[#17D4C3]"
                                                                : "text-slate-500"
                                                        }
                                                    />

                                                    <span className="flex-1 font-medium">
                                {item.label}
                            </span>

                                                    {item.badge !== undefined && (
                                                        <span className="text-[10px] font-semibold bg-[#17D4C3]/15 text-[#17D4C3] rounded-full px-1.5 py-0.5">
                                    {item.badge}
                                </span>
                                                    )}

                                                    {item.dot && (
                                                        <span className="w-1.5 h-1.5 rounded-full bg-red-500" />
                                                    )}

                                                    {isActive && (
                                                        <span className="absolute left-0 w-1 h-5 rounded-r-full bg-[#17D4C3]" />
                                                    )}
                                                </>
                                            )}
                                        </NavLink>
                                    </li>
                                );
                            })}
                        </ul>
                    </div>
                ))}
            </nav>

            {/* Bottom: auth-aware */}
            <div className="p-2.5 border-t border-white/[0.06] shrink-0">
                {isAuthenticated ? (
                    <div className="flex items-center gap-2.5 px-2 py-2 rounded-lg border border-white/[0.06] bg-white/[0.02] hover:bg-white/[0.04] transition-colors duration-200">
                        <div className="w-8 h-8 rounded-full bg-[#17D4C3]/15 border border-[#17D4C3]/30 flex items-center justify-center text-[#17D4C3] font-semibold text-[11px] shrink-0">
                            {userInitials}
                        </div>
                        <div className="leading-tight min-w-0 flex-1">
                            <p className="text-white text-[13px] font-medium truncate">{userName}</p>
                            <p className="text-slate-500 text-[11px] truncate">{userRole}</p>
                        </div>
                        <button
                            onClick={onLogout}
                            aria-label="Log out"
                            className="w-7 h-7 rounded-md flex items-center justify-center text-slate-500 hover:text-red-400 hover:bg-red-400/10 transition-colors duration-200 shrink-0"
                        >
                            <LogOut size={14} />
                        </button>
                    </div>
                ) : (
                    <button
                        onClick={onLogin}
                        className="w-full flex items-center justify-center gap-2 px-3 py-2.5 rounded-lg bg-[#17D4C3] text-[#081423] text-[13px] font-semibold hover:bg-[#17D4C3]/90 transition-colors duration-200"
                    >
                        <LogIn size={15} />
                        Log in
                    </button>
                )}
            </div>
        </div>
    );
}