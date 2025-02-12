from sqlalchemy import Column, Integer, String, DECIMAL, ForeignKey
from sqlalchemy.orm import relationship
from .database import Base

class Order(Base):
    __tablename__ = "Orders"  

    id = Column("Id", Integer, primary_key=True, index=True)  
    price = Column("Price", Integer, nullable=False)
    description = Column("Description", String, nullable=False) 
    status = Column("Status", String, default="Aguardando pagamento", nullable=False)  
    user_id = Column("UserId", Integer, ForeignKey("Users.Id"), nullable=False) 

    User = relationship("User", back_populates="Orders")

    def __init__(self, price, description, user_id, status="Aguardando pagamento"):
        self.price = price
        self.description = description
        self.user_id = user_id
        self.status = status


class User(Base):
    __tablename__ = "Users"  

    id = Column("Id", Integer, primary_key=True, index=True)  
    name = Column("Name", String, nullable=False)  
    email = Column("Email", String, unique=True, nullable=False)
    password = Column("Password", String, nullable=False)

    Orders = relationship("Order", back_populates="User")  

    def __init__(self, name, Email, password):
        self.name = name
        self.email = email
        self.password = password