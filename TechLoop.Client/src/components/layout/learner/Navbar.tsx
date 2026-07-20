import React, { useEffect, useRef, useState } from "react";
import { Search, Bell, ChevronRight, ChevronDown, Plus, User, Settings, LogOut } from "lucide-react";

interface NavbarProps {
    hidden?: boolean;

    breadcrumb?: string[];
    userName?: string;
    userInitials?: string;
    notificationCount?: number;
    onSearch?: (query: string) => void;
    onQuickAction?: () => void;
    onOpenNotifications?: () => void;
    onOpenProfile?: () => void;
    onOpenSettings?: () => void;
    onLogout?: () => void;
}

export const Navbar: React.FC<NavbarProps> = ({
                                                  hidden = false,

                                                  breadcrumb = ["Dashboard"],
                                                  userName = "Arjun Mehta",
                                                  userInitials = "AM",
                                                  notificationCount = 3,
                                                  onSearch,
                                                  onQuickAction,
                                                  onOpenNotifications,
                                                  onOpenProfile,
                                                  onOpenSettings,
                                                  onLogout,
                                              }) => {
    const [menuOpen, setMenuOpen] = useState(false);
    const menuRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        function handleClickOutside(e: MouseEvent) {
            if (menuRef.current && !menuRef.current.contains(e.target as Node)) {
                setMenuOpen(false);
            }
        }
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    return (
        <header
            className={`
        fixed
        top-0
        left-64
        right-0
        z-50
        h-16
        grid
        grid-cols-[1fr_auto_1fr]
        items-center
        gap-4
        px-5
        bg-[#0A1930]/80
        backdrop-blur-xl
        border-b
        border-white/5
        transition-transform
        duration-300
        ${
                hidden
                    ? "-translate-y-full"
                    : "translate-y-0"
            }
    `}
        >
            {/* Breadcrumb */}
            <div className="hidden sm:flex items-center gap-1.5 text-[13px] text-slate-400 min-w-0">
                {breadcrumb.map((crumb, i) => {
                    const isLast = i === breadcrumb.length - 1;
                    return (
                        <React.Fragment key={crumb}>
                            {i > 0 && <ChevronRight className="w-3 h-3 text-slate-600 shrink-0" />}
                            <span className={`truncate ${isLast ? "text-white font-medium" : ""}`}>
                                {crumb}
                            </span>
                        </React.Fragment>
                    );
                })}
            </div>

            {/* Search */}
            <div className="w-full max-w-[420px] justify-self-center relative col-start-1 sm:col-start-2">
                <Search className="w-[14px] h-[14px] text-slate-500 absolute left-3 top-1/2 -translate-y-1/2" />
                <input
                    type="text"
                    placeholder="Search problems, topics, people…"
                    onChange={(e) => onSearch?.(e.target.value)}
                    className="
            w-full bg-[#11243A] border border-white/[0.06] rounded-xl
            pl-9 pr-14 py-2 text-[13px] text-white font-sans
            placeholder:text-slate-500
            focus:outline-none focus:border-[#17D4C3]/50 focus:ring-1 focus:ring-[#17D4C3]/20
            transition-all duration-200"
                />
                {/*<span className="absolute right-2.5 top-1/2 -translate-y-1/2 text-[10px] text-slate-500 border border-white/[0.08] rounded-md px-1.5 py-0.5 font-mono">*/}
                {/*    */}
                {/*</span>*/}
            </div>

            {/* Actions */}
            <div className="flex items-center justify-end gap-2 col-start-2 sm:col-start-3">
                <button
                    onClick={onQuickAction}
                    className="
            hidden md:flex items-center gap-1.5 px-3 py-2 rounded-xl
            bg-[#17D4C3]/10 border border-[#17D4C3]/25
            text-[#17D4C3] text-[12.5px] font-medium
            hover:bg-[#17D4C3]/15 transition-colors duration-200
          "
                >
                    <Plus className="w-3.5 h-3.5" />
                    New
                </button>

                <button
                    onClick={onOpenNotifications}
                    aria-label="Notifications"
                    className="
            relative w-9 h-9 rounded-xl border border-white/[0.06]
            flex items-center justify-center
            text-slate-400 hover:text-[#17D4C3] hover:border-[#17D4C3]/30 hover:bg-white/[0.03]
            transition-all duration-200
          "
                >
                    <Bell className="w-4 h-4" />
                    {notificationCount > 0 && (
                        <span className="absolute -top-1 -right-1 w-4 h-4 rounded-full bg-red-500 text-white text-[9px] font-bold flex items-center justify-center">
                            {notificationCount}
                        </span>
                    )}
                </button>

                <div className="w-px h-6 bg-white/[0.06]" />

                {/* Profile dropdown */}
                <div className="relative" ref={menuRef}>
                    <button
                        onClick={() => setMenuOpen((v) => !v)}
                        aria-label="Open profile menu"
                        className="flex items-center gap-1.5 pl-1 pr-2 py-1 rounded-full hover:bg-white/[0.05] transition-colors duration-200"
                    >
                        <div className="w-7 h-7 rounded-full border border-white/[0.08] bg-[#17D4C3]/15 text-[#17D4C3] flex items-center justify-center text-[11px] font-bold">
                            {userInitials}
                        </div>
                        <ChevronDown
                            className={`w-3.5 h-3.5 text-slate-500 transition-transform duration-200 ${
                                menuOpen ? "rotate-180" : ""
                            }`}
                        />
                    </button>

                    {menuOpen && (
                        <div
                            className="
                absolute right-0 mt-2 w-56 rounded-xl overflow-hidden z-50
                bg-[#11243A] border border-white/[0.08] shadow-xl shadow-black/40
                animate-in fade-in slide-in-from-top-1 duration-200
              "
                        >
                            <div className="px-3.5 py-3 border-b border-white/[0.06]">
                                <p className="text-white text-[13px] font-medium truncate">{userName}</p>
                                <p className="text-slate-500 text-[11px]">View your profile</p>
                            </div>
                            <div className="p-1.5">
                                <button
                                    onClick={() => { onOpenProfile?.(); setMenuOpen(false); }}
                                    className="w-full flex items-center gap-2.5 px-2.5 py-2 rounded-lg text-[13px] text-slate-300 hover:bg-white/[0.05] hover:text-white transition-colors duration-200"
                                >
                                    <User className="w-4 h-4 text-slate-500" />
                                    Profile
                                </button>
                                <button
                                    onClick={() => { onOpenSettings?.(); setMenuOpen(false); }}
                                    className="w-full flex items-center gap-2.5 px-2.5 py-2 rounded-lg text-[13px] text-slate-300 hover:bg-white/[0.05] hover:text-white transition-colors duration-200"
                                >
                                    <Settings className="w-4 h-4 text-slate-500" />
                                    Settings
                                </button>
                            </div>
                            <div className="p-1.5 border-t border-white/[0.06]">
                                <button
                                    onClick={() => { onLogout?.(); setMenuOpen(false); }}
                                    className="w-full flex items-center gap-2.5 px-2.5 py-2 rounded-lg text-[13px] text-red-400 hover:bg-red-400/10 transition-colors duration-200"
                                >
                                    <LogOut className="w-4 h-4" />
                                    Log out
                                </button>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </header>
    );
};

export default Navbar;