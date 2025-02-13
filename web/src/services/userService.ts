import config from "../config.ts";

export async function getUser() {
  try {

    const id = localStorage.getItem("user-id");
    const token = localStorage.getItem("token");

    const response = await fetch(`${config.apiUrl}/user?id=${id}`, {
      headers: { "Authorization": `Bearer ${token}` },
      method: "GET",
    });

    if (!response.ok) {
      throw new Error("Falha ao buscar usuário.");
    }

    const result = await response.json();
    return result.data;
  } catch (error) {
    console.error("Erro ao buscar usuário:", error);
    throw error;
  }
}

export async function updateUser(name: string, email: string, password: string) {
  try {
    const id = localStorage.getItem("user-id");
    const token = localStorage.getItem("token");

    const response = await fetch(`${config.apiUrl}/user/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`,
      },
      body: JSON.stringify({ name, email, password }),
    });

    if (!response.ok) {
      throw new Error("Falha ao atualizar usuário.");
    }

    const result = await response.json();
    return result.data;
  } catch (error) {
    console.error("Erro ao atualizar usuário:", error);
    throw error;
  }
}

export async function deleteUser() {
  try {
    const id = localStorage.getItem("user-id");
    const token = localStorage.getItem("token");

    const response = await fetch(`${config.apiUrl}/user/${id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      throw new Error("Falha ao atualizar usuário.");
    }

    const result = await response.json();
    return result.data;
  } catch (error) {
    console.error("Erro ao atualizar usuário:", error);
    throw error;
  }
}