import { AtSign, MessageSquare, Code2 } from "lucide-react";

interface FooterLink {
    label: string;
    href: string;
}

interface FooterColumn {
    title: string;
    links: FooterLink[];
}

const columns: FooterColumn[] = [
    {
        title: "Product",
        links: [
            { label: "Practice", href: "#" },
            { label: "Classrooms", href: "#" },
            { label: "AI Mentor", href: "#" },
            { label: "Leaderboard", href: "#" },
        ],
    },
    {
        title: "Resources",
        links: [
            { label: "Documentation", href: "#" },
            { label: "API reference", href: "#" },
            { label: "Community", href: "#" },
            { label: "Status", href: "#" },
        ],
    },
    {
        title: "Company",
        links: [
            { label: "About", href: "#" },
            { label: "Privacy", href: "#" },
            { label: "Terms", href: "#" },
            { label: "Contact", href: "#" },
        ],
    },
];

const socials = [
    { icon: AtSign, href: "#", label: "Email" },
    { icon: MessageSquare, href: "#", label: "Community forum" },
    { icon: Code2, href: "#", label: "GitHub" },
];

export default function Footer() {
    return (
        <footer className="w-full border-t border-white/5 bg-[#0a1224]/80 backdrop-blur-md font-sans">
            <div className="max-w-7xl mx-auto px-6 py-16">
                <div className="grid grid-cols-2 md:grid-cols-5 gap-10">
                    {/* Brand */}
                    <div className="col-span-2 space-y-4">
                        <div className="flex items-center gap-2">
                            <div className="w-8 h-8 bg-teal-400 rounded-lg flex items-center justify-center text-[#0a1224] font-bold text-sm">
                                TL
                            </div>
                            <h2 className="text-xl font-bold text-white">TechLoop</h2>
                        </div>
                        <p className="text-sm leading-relaxed text-slate-400 max-w-xs">
                            Where AI assistance meets human expertise. Practice, learn, and
                            ship with a developer platform built for depth of focus.
                        </p>
                        <div className="flex items-center gap-2 pt-1">
                            {socials.map(({ icon: Icon, href, label }) => (
        
                                <a key={label}
                                href={href}
                                aria-label={label}
                                className="w-8 h-8 rounded-lg border border-white/5 flex items-center justify-center text-slate-400 hover:text-teal-400 hover:border-teal-400/40 transition-colors"
                                >
                                <Icon size={16} strokeWidth={1.5} />
                                </a>
                                ))}
                        </div>
                    </div>

                    {/* Link columns */}
                    {columns.map((col) => (
                        <nav key={col.title} className="space-y-3">
                            <p className="text-[11px] font-semibold text-slate-500 uppercase tracking-widest mb-2">
                                {col.title}
                            </p>
                            <ul className="flex flex-col gap-2">
                                {col.links.map((link) => (
                                    <li key={link.label}>

                                       <a href={link.href}
                                        className="text-sm text-slate-400 hover:text-teal-400 transition-colors"
                                        >
                                        {link.label}
                                    </a>
                                    </li>
                                    ))}
                            </ul>
                        </nav>
                        ))}
                </div>

                {/* Bottom bar */}
                <div className="mt-16 pt-6 border-t border-white/5 flex flex-col md:flex-row items-center justify-between gap-4">
                    <p className="text-[11px] text-slate-500">
                        © {new Date().getFullYear()} TechLoop. All rights reserved.
                    </p>
                    <div className="flex items-center gap-2">
                        <span className="w-1.5 h-1.5 rounded-full bg-teal-400" />
                        <span className="text-[11px] text-slate-500 font-mono">
                            v2.4.1 · all systems operational
                        </span>
                    </div>
                </div>
            </div>
        </footer>
    );
}