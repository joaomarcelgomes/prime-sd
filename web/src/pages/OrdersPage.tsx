import React, { useState } from "react";

import { MenuLayout } from "../layouts/MenuLayout";
import { useOrders } from "../hooks/useOrders";

type Order = {
    id: string;
    value: string;
    description: string;
    status: string;
};

type ModalCreateProps = {
    isOpen: boolean;
    onClose: () => void;
};

type ModalDetailProps = {
    isOpen: boolean;
    onClose: () => void;
    order: Order | null;
};

const OrdersPage = () => {
    const [isModalDetailOpen, setIsModalDetailOpen] = useState(false);
    const [isModalCreateOrderOpen, setIsModalCreateOrderOpen] = useState(false);
    const { orders, isLoading, error } = useOrders();
    const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);

    return (
        <MenuLayout nav="Orders">
            <div className="flex-1 flex items-center flex-col">
                <h2 className="text-xl font-semibold my-8">Pedidos em Andamento</h2>

                {isLoading ? (
                    <p>Carregando pedidos...</p>
                ) : error ? (
                    <p>Ocorreu um erro ao carregar os pedidos.</p>
                ) : orders.length == 0 ? (
                    <p>Nenhum pedido encontrado.</p>
                ) : (
                    <div className="w-full max-w-4xl mx-auto bg-gray-200 p-4 rounded-lg">
                        <table className="w-full">
                            <thead>
                                <tr>
                                    <th className="p-2 text-left font-semibold">ID do Pedido</th>
                                    <th className="p-2 text-left font-semibold">Valor do Pedido</th>
                                    <th className="p-2 text-left font-semibold">Descrição do Pedido</th>
                                    <th className="p-2 text-left font-semibold">Status do Pedido</th>
                                    <th className="p-2 text-left font-semibold">Ação</th>
                                </tr>
                            </thead>
                            <tbody>
                                {orders.map((order) => (
                                    <tr key={order.id} className="border-t">
                                        <td className="p-2">{order.id}</td>
                                        <td className="p-2">{order.value}</td>
                                        <td className="p-2 truncate max-w-xs">{order.description}</td>
                                        <td className="p-2 truncate max-w-xs">{order.status}</td>
                                        <td className="p-2">
                                            <button className="text-blue-600 hover:underline cursor-pointer"
                                                onClick={() => {
                                                    setSelectedOrder(order);
                                                    setIsModalDetailOpen(true);
                                                }}
                                            >
                                                Ver Detalhes
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        <button className="fixed bottom-6 right-6 bg-gray-600 text-white p-4 rounded-md shadow-lg hover:bg-gray-700"
                        onClick={() => setIsModalCreateOrderOpen(true)}>
                            Criar Pedido
                        </button>
                    </div>
                )}
            </div>

            {isModalDetailOpen && (
                <DetailComponent 
                    isOpen={isModalDetailOpen}
                    onClose={() => setIsModalDetailOpen(false)}
                    order={selectedOrder}
                />
            )}

            {isModalCreateOrderOpen && (
                <CreateOrderComponent
                    isOpen={isModalCreateOrderOpen}
                    onClose={() => setIsModalCreateOrderOpen(false)}
                />
            )}

        </MenuLayout>
    );
};

const CreateOrderComponent: React.FC<ModalCreateProps> = ({ isOpen, onClose }) => {

    const [value, setValue] = useState<string>("");
    const [description, setDescription] = useState<string>("");

    const handleValueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const inputValue = e.target.value;

        if (/^\d*([.,]?\d{0,2})?$/.test(inputValue)) {
        setValue(inputValue.replace(",", "."));
        }
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log({ value, description });
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-gray-500/1 transition-opacity bg-opacity-30 backdrop-blur-sm flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg shadow-lg w-96 relative">
            <button
              className="absolute top-3 right-4 text-lg font-bold"
              onClick={onClose}
            >
              ×
            </button>
            <h2 className="text-xl font-semibold text-center mb-4">Criar Pedido</h2>

            <form className="space-y-3">
              <div>
                <label className="block text-sm font-medium">Valor</label>
                <input
                    type="text"
                    className="w-full p-2 border rounded-md bg-white"
                    value={value}
                    onChange={handleValueChange}
                    placeholder="Digite o valor (ex: 199.99)"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">Descrição</label>
                <textarea
                    className="w-full p-2 border rounded-md bg-white resize-none"
                    rows={4}
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    placeholder="Descreva o pedido..."
                />
              </div>
              <button
                type="submit"
                className="mt-4 w-full bg-gray-600 text-white py-2 rounded-lg hover:bg-gray-700"
                onClick={handleSubmit}
              >
                Criar Pedido
              </button>
            </form>
          </div>
        </div>
    );
}

const DetailComponent: React.FC<ModalDetailProps> = ({ isOpen, onClose, order }) => {
    if (!isOpen || !order) return null;

    return (
        <div className="fixed inset-0 bg-gray-500/1 transition-opacity bg-opacity-30 backdrop-blur-sm flex items-center justify-center">
            <div className="bg-white p-6 rounded-lg shadow-lg w-96 relative">
                <button
                    className="absolute top-3 right-4 text-lg font-bold"
                    onClick={onClose}
                >
                    ×
                </button>
                <h2 className="text-lg font-semibold text-center mb-4">Detalhes do Pedido</h2>
                <p className="text-sm font-medium">Pedido ID</p>
                <p>{order!.id}</p>
                <p className="text-sm font-medium mt-2">Pedido Valor</p>
                <p>{order!.value}</p>
                <p className="text-sm font-medium mt-2">Descrição</p>
                <p>{order!.description}</p>
                <p className="text-sm font-medium mt-2">Status do Pedido</p>
                <p>{order!.status}</p>
            </div>
        </div>
    )
}

export default OrdersPage;
