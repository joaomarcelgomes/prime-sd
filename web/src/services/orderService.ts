import config from "../config.ts";

export type Order = {
    id: string;
    value: string;
    description: string;
    status: string;
};
  
export const fetchOrders = async (): Promise<Order[]> => {
  try {
    // const response = await fetch("https://api.example.com/orders");
    // if (!response.ok) {
    //   throw new Error("Erro ao buscar pedidos");
    // }

    return [
        {
            id: "1",
            value: "R$ 100,00",
            description: "Pedido de teste",
            status: "Em andamento",
        },
        {
            id: "2",
            value: "R$ 200,00",
            description: "Pedido de teste 2",
            status: "Em andamento",
        },
    ];

    // return await response.json();
  } catch (error) {
    console.error(error);
    throw error;
  }
};

export const createOrder = async (value: string, description: string): Promise<Order> => {
  try {

    const token = localStorage.getItem("token");

    const response = await fetch(`${config.apiUrl}/order`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`,
      },
      body: JSON.stringify({ value, description }),
    });
    
    return await response.json();
  } catch (error) {
    console.error(error);
    throw error;
  }
}