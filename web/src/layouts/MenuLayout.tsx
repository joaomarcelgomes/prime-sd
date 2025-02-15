interface MenuLayoutProps {
    nav: string;
    children: React.ReactNode;
}

export function MenuLayout({ nav, children }: MenuLayoutProps) {
    const selected = "font-medium text-[#29638A]";
    const notSelected = "cursor-pointer text-[#29638A]";

    return (
        <div className="w-full h-screen bg-white flex flex-col">
            <div className="bg-white text-white p-4 flex justify-end border-b border-[#29638A]">
                <div className="flex gap-4">
                <a href="/orders" className={nav == "Orders"? selected : notSelected}>Pedidos</a>
                <a href="/profile" className={nav == "Profile"? selected : notSelected}>Perfil</a>
                </div>
            </div>
            {children}
        </div>
    )
}