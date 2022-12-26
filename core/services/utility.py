from motor import motor_asyncio
from dotenv import load_dotenv
import os


def get_db_client():
    load_dotenv()
    client = motor_asyncio.AsyncIOMotorClient(os.getenv("LINDDEVDOTCOM_DB_CONN"))

    return client.linddevdotcom
