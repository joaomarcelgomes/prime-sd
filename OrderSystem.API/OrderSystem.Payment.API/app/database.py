from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
import os

# Obtendo configurações do ambiente (importante para Docker)
POSTGRES_USER = os.getenv("POSTGRES_USER", "prime")
POSTGRES_PASSWORD = os.getenv("POSTGRES_PASSWORD", "prime")
POSTGRES_DB = os.getenv("POSTGRES_DB", "order_system")  # ⚠️ Sem hífen no nome do banco
POSTGRES_HOST = os.getenv("POSTGRES_HOST", "postgres")  # O nome do serviço no Docker Compose
POSTGRES_PORT = os.getenv("POSTGRES_PORT", "5432")

# Criando a URL do banco de dados
DATABASE_URL = f"postgresql://{POSTGRES_USER}:{POSTGRES_PASSWORD}@{POSTGRES_HOST}:{POSTGRES_PORT}/{POSTGRES_DB}"

# Criando o engine e a sessão do SQLAlchemy
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base = declarative_base()

# Função para criar tabelas automaticamente no banco
def init_db():
    from models import Base  # Importa os modelos antes de criar as tabelas
    Base.metadata.create_all(bind=engine)

# Executa a criação das tabelas
init
