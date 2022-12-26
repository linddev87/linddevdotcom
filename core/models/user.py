from pydantic import BaseModel, Field


class User(BaseModel):
    username: str = Field(...)
    full_name: str = Field(...)
    disabled: bool = Field(...)


class UserInDB(User):
    hashed_password: str = Field(...)
