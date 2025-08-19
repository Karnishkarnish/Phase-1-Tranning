from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker, Session

from fastapi import FastAPI, Depends, HTTPException
import uvicorn
from pydantic import BaseModel
from typing import List

SQLALCHEMY_DATABASE_URL = "sqlite:///D:/SQL/SongDB.db"
engine = create_engine(
    SQLALCHEMY_DATABASE_URL,
    echo=True,
    connect_args={"check_same_thread": False}
)

Base = declarative_base()

class User(Base):
    __tablename__ = "Users"
    Id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    Name = Column(String, nullable=False, unique=True)


class Song(Base):
    __tablename__ = "Song"
    Id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    Name = Column(String, nullable=False)
    Duration = Column(Integer, nullable=False)


Base.metadata.create_all(bind=engine)


SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)


app = FastAPI()



def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

class SongCreate(BaseModel):
    Name: str
    Duration: int
    class Config:
        from_attributes = True


class SongResponse(BaseModel):
    Id: int
    Name: str
    Duration: int
    class Config:
        from_attributes = True


class UserCreate(BaseModel):
    Name: str


class UserResponse(BaseModel):
    Id: int
    Name: str
    class Config:
        from_attributes = True


@app.post("/add_user", response_model=UserResponse)
def add_user(user: UserCreate, db: Session = Depends(get_db)):
    existing = db.query(User).filter(User.Name == user.Name).first()
    if existing:
        raise HTTPException(status_code=400, detail="User already exists")
    u = User(Name=user.Name)
    db.add(u)
    db.commit()
    db.refresh(u)
    return u

@app.post("/{username}/add_song", response_model=SongResponse)
def add_song(username: str, song: SongCreate, db: Session = Depends(get_db)):
    user = db.query(User).filter(User.Name == username).first()
    if not user:
        raise HTTPException(status_code=401, detail="No user found - Unauthorized access")

    s = Song(Name=song.Name, Duration=song.Duration)
    db.add(s)
    db.commit()
    db.refresh(s)
    return s

@app.get("/{username}/songs", response_model=List[SongResponse])
def get_all(username: str, db: Session = Depends(get_db)):
    user = db.query(User).filter(User.Name == username).first()
    if not user:
        raise HTTPException(status_code=401, detail="No user found - Unauthorized access")

    recs = db.query(Song).all()
    return recs

@app.get("/{username}/song/{song_id}", response_model=SongResponse)
def get_by_id(username: str, song_id: int, db: Session = Depends(get_db)):
    user = db.query(User).filter(User.Name == username).first()
    if not user:
        raise HTTPException(status_code=401, detail="No user found - Unauthorized access")

    s = db.query(Song).filter_by(Id=song_id).first()
    if not s:
        raise HTTPException(status_code=404, detail="Song not found")
    return s


if __name__ == "__main__":
    uvicorn.run("app:app", host="127.0.0.1", port=8000, reload=True)
