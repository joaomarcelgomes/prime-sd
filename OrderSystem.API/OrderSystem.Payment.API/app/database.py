from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
import os

db_path = os.path.abspath("C:/Users/kleni/OneDrive/Desktop/Workspace/prime-sd/OrderSystem.API/OrderSystem.Order.API/order-system.db")
DATABASE_URL = f"sqlite:///{db_path}"

engine = create_engine(DATABASE_URL, connect_args={"check_same_thread": False})
SessionLocal = sessionmaker(bind=engine)
Base = declarative_base()

Base.metadata.create_all(bind=engine)