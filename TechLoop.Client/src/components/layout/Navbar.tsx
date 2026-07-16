import React from "react";
import { Search, Bell, Moon, ChevronRight } from "lucide-react";

interface NavbarProps {
    breadcrumb?: string[];
    userInitials?: string;
    notificationCount?: number;
    onSearch?: (query: string) => void;
    onToggleTheme?: () => void;
    onOpenNotifications?: () => void;
    onOpenProfile?: () => void;
}

export const Navbar: React.FC<NavbarProps> = ({
                                                  breadcrumb = ["TechLoop", "Dashboard"],
                                                  userInitials = "AM",
                                                  notificationCount = 3,
                                                  onSearch,
                                                  onToggleTheme,
                                                  onOpenNotifications,
                                                  onOpenProfile,
                                              }) => {
    return (
        <header
            className="
        h-16 flex items-center justify-between px-6
        bg-bg/85 backdrop-blur-md
        border-b border-border
      "
        >
            {/* Breadcrumb */}
            <div className="flex items-center gap-2 text-[13px] text-secondary shrink-0">
                {breadcrumb.map((crumb, i) => {
                    const isLast = i === breadcrumb.length - 1;
                    return (
                        <React.Fragment key={crumb}>
                            {i > 0 && <ChevronRight className="w-3 h-3 text-muted" />}
                            <span className={isLast ? "text-white font-medium" : ""}>
                {crumb}
              </span>
                        </React.Fragment>
                    );
                })}
            </div>

            {/* Search */}
            <div className="flex-1 max-w-[420px] mx-8 relative">
                <Search className="w-[15px] h-[15px] text-muted absolute left-3 top-1/2 -translate-y-1/2" />
                <input
                    type="text"
                    placeholder="Search problems, topics, people…"
                    onChange={(e) => onSearch?.(e.target.value)}
                    className="
            w-full bg-card border border-border rounded-xl
            pl-9 pr-14 py-2.5 text-[13px] text-white font-sans
            placeholder:text-muted
            focus:outline-none focus:border-accent/50
            transition-colors
          "
                />
                <span
                    className="
            absolute right-2.5 top-1/2 -translate-y-1/2
            text-[10px] text-muted border border-border rounded-md px-1.5 py-0.5
          "
                >
          ⌘K
        </span>
            </div>

            {/* Actions */}
            <div className="flex items-center gap-3.5 shrink-0">
                <button
                    onClick={onToggleTheme}
                    aria-label="Toggle theme"
                    className="
            w-9 h-9 rounded-xl border border-border
            flex items-center justify-center
            text-secondary hover:text-accent hover:border-accent/40
            transition-colors
          "
                >
                    <Moon className="w-4 h-4" />
                </button>

                <button
                    onClick={onOpenNotifications}
                    aria-label="Notifications"
                    className="
            relative w-9 h-9 rounded-xl border border-border
            flex items-center justify-center
            text-secondary hover:text-accent hover:border-accent/40
            transition-colors
          "
                >
                    <Bell className="w-4 h-4" />
                    {notificationCount > 0 && (
                        <span className="absolute -top-1 -right-1 w-4 h-4 rounded-full bg-danger text-white text-[9px] font-bold flex items-center justify-center">
              {notificationCount}
            </span>
                    )}
                </button>

                <div className="w-px h-6 bg-border" />

                <button
                    onClick={onOpenProfile}
                    aria-label="Open profile menu"
                    className="w-8 h-8 rounded-full border border-border bg-accent/15 text-accent flex items-center justify-center text-[11px] font-bold hover:opacity-90 transition-opacity">
                    {userInitials}
                </button>
            </div>
        </header>
    );
};

export default Navbar;