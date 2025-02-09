export async function loginUser(email: string, password: string) {
  try {
    const response = await fetch("/api/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      throw new Error("Falha na autenticação. Verifique suas credenciais.");
    }

    const data = await response.json();
    localStorage.setItem("token", data.token);
    return data;
  } catch (error) {
    console.error("Erro no login:", error);
    throw error;
  }
}

export async function registerUser(name: string, email: string, password: string) {
  try {
    const response = await fetch("/api/register", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name, email, password }),
    });

    if (!response.ok) {
      throw new Error("Falha ao criar conta. Verifique os dados.");
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Erro ao registrar usuário:", error);
    throw error;
  }
}

export function logoutUser() {
  localStorage.removeItem("token");
}
