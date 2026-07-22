import { useEffect, useMemo, useState } from "react";
import LearningHeader from "./components/LearningHeader";
import SearchBar from "./components/SearchBar";
import CategoryTabs from "./components/CategoryTabs";
import TechGrid from "./components/TechGrid";
import technologyService from "../../services/technologyService";
import type { Technology } from "../../types/technology";
import { LoaderBar } from "../../components/common/Loader";
import EmptyState from "../../components/common/EmptyState";
import technologyCategoryService from "../../services/technologyCategoryService";
import type { TechnologyCategory } from "../../types/technologyCategory";



export default function Learn() {
    const [categories, setCategories] = useState<TechnologyCategory[]>([]);
    const [technologies, setTechnologies] = useState<Technology[]>([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState("");
    const [selectedCategory, setSelectedCategory] = useState<number | null>(null);

    useEffect(() => {
        void loadData();
    }, []);

    async function loadData() {
        try {
            const technologiesData = await technologyService.getAll();setTechnologies(technologiesData);
            try {
                const categoriesData = await technologyCategoryService.getAll();setCategories(categoriesData);
                if (categoriesData.length > 0) {
                    setSelectedCategory(categoriesData[0].id);
                }
            } catch (error) {
                console.warn("Unable to load categories.", error);
            }
        } finally {
            setLoading(false);
        }
    }

    const filteredTechnologies = useMemo(() => {
        return technologies.filter((technology) =>
            technology.name
                .toLowerCase()
                .includes(search.toLowerCase())
        );
    }, [technologies, search]);

    return (
        <div className="mx-auto w-full max-w-7xl px-8 py-8">

            {/* Header */}
            <LearningHeader />

            {/* Search */}
            <div className="mt-8">
                <SearchBar
                    value={search}
                    onChange={setSearch}
                />
            </div>

            {/* Categories */}
            <div className="mt-6">
                <CategoryTabs
                    categories={categories}
                    selectedCategory={selectedCategory}
                    onCategoryChange={setSelectedCategory}
                />
            </div>

            {/* Section Header */}
            <div className="mt-10 flex items-center justify-between border-b border-slate-800 pb-4">
                <h2 className="text-xs font-semibold uppercase tracking-[0.2em] text-slate-400">
                    All Technologies
                </h2>

                <span className="text-xs text-slate-500">
                    {filteredTechnologies.length} Technologies
                </span>
            </div>

            {/* Content */}
            <div className="mt-8">
                {loading && <LoaderBar />}

                {!loading && filteredTechnologies.length === 0 && (
                    <EmptyState
                        title="No Technologies"
                        description="No technologies found."
                    />
                )}

                {!loading && filteredTechnologies.length > 0 && (
                    <TechGrid
                        technologies={filteredTechnologies}
                    />
                )}
            </div>
        </div>
    );
}