import { useState } from "react";

import { MenuLayout } from "../layouts/MenuLayout";

const ProfilePage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <MenuLayout nav="Profile">
      <div className="flex-1 flex items-center flex-col">
        <h2 className="text-xl font-semibold my-16">Perfil</h2>
        <div className="bg-white rounded-xl p-6 w-96 text-center border border-neutral-500">

          <div className="text-gray-700 space-y-1">
            <p className="text-sm">Nome</p>
            <p className="font-medium">Maria Isabely</p>

            <p className="text-sm mt-4">E-Mail</p>
            <p className="font-medium">m.isa@gmail.com</p>
          </div>

          <button className="mt-6 w-full bg-neutral-500 text-black py-2 rounded-lg hover:bg-neutral-700" onClick={() => setIsModalOpen(true)}>
            Editar Perfil
          </button>
          <button className="mt-4 text-red-500 text-sm hover:underline">
            Excluir Conta
          </button>
        </div>
      </div>

      {isModalOpen && (
        <div className="fixed inset-0 bg-gray-500/1 transition-opacity bg-opacity-30 backdrop-blur-sm flex items-center justify-center">
          <div className="bg-gray-200 p-6 rounded-lg w-96 relative shadow-lg">
            <button
              className="absolute top-3 right-4 text-lg font-bold"
              onClick={() => setIsModalOpen(false)}
            >
              ×
            </button>
            <h2 className="text-xl font-semibold text-center mb-4">Editar Perfil</h2>

            <form className="space-y-3">
              <div>
                <label className="block text-sm font-medium">Nome</label>
                <input
                  type="text"
                  className="w-full p-2 border rounded-md bg-gray-100"
                  placeholder="Digite seu nome"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">E-Mail</label>
                <input
                  type="email"
                  className="w-full p-2 border rounded-md bg-gray-100"
                  placeholder="Digite seu e-mail"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">Senha</label>
                <input
                  type="password"
                  className="w-full p-2 border rounded-md bg-gray-100"
                  placeholder="Digite sua senha"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">Confirmar senha</label>
                <input
                  type="password"
                  className="w-full p-2 border rounded-md bg-gray-100"
                  placeholder="Confirme sua senha"
                />
              </div>
              <button
                type="submit"
                className="mt-4 w-full bg-gray-500 text-white py-2 rounded-lg hover:bg-gray-600"
              >
                Salvar Alterações
              </button>
            </form>
          </div>
        </div>
      )}
    </MenuLayout>
  );
};


export default ProfilePage