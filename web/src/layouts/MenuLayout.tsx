interface MenuLayoutProps {
    nav: string;
    children: React.ReactNode;
}

export function MenuLayout({ nav, children }: MenuLayoutProps) {

    const selected = "text-black font-medium";
    const notSelected = "cursor-pointer text-neutral-700 hover:text-white";

    return (
        <div className="w-full h-screen bg-white flex flex-col">
            <div className="bg-neutral-300 text-white p-4 flex justify-end">
                <div className="flex gap-4">
                <a href="/orders" className={nav == "Orders"? selected : notSelected}>Pedidos</a>
                <a href="/profile" className={nav == "Profile"? selected : notSelected}>Perfil</a>
                </div>
            </div>
            {children}
        </div>
    )
}