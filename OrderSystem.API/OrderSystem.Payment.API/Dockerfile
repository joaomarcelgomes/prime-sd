FROM python:3.10
WORKDIR /app

# Copia o arquivo de dependências
COPY requirements.txt ./

# Garante que o pip está atualizado antes da instalação
RUN pip install --upgrade pip

# Instala as dependências do Flask
RUN pip install --no-cache-dir -r requirements.txt

# Copia todo o código-fonte
COPY . .

# Expõe a porta do Flask
EXPOSE 8000

# Define a variável de ambiente para rodar Flask corretamente
ENV FLASK_APP=app.py
ENV FLASK_RUN_HOST=0.0.0.0
ENV FLASK_RUN_PORT=8000

# Inicia o Flask
CMD ["flask", "run"]
