import datetime
from core.services.utility import get_db_client


class BaseRepo:
    def __init__(self, entity_type: str):
        self.db = get_db_client()
        self.entity_type: str = entity_type

    async def get(self, _id: str):
        try:
            entry = await self.get_by_prop("_id", _id)

            return entry
        except Exception as e:
            print(e)

    async def get_by_prop(self, prop_name: str, value):
        try:
            entry = await self.db[self.entity_type].find_one({prop_name, value})

            return entry
        except Exception as e:
            print(e)

    async def delete(self, _id: str):
        try:
            delete_result = await self.db[self.entity_type].delete_one({"_id": _id})

            if delete_result.acknowledged:
                return True
            else:
                return False
        except Exception as e:
            print(e)

    async def list(self):
        try:
            test = self.db[self.entity_type]

            entries = await test.find().to_list(1000)

            return entries
        except Exception as e:
            print(e)

    async def create(self, entity):
        try:
            insert_result = await self.db[self.entity_type].insert_one(entity)

            if insert_result.acknowledged:
                return insert_result.inserted_id
            else:
                return None
        except Exception as e:
            print(e)

    async def update(self, entity):
        try:
            existing_entity = await self.get(entity.id)

            if existing_entity is None:
                return None

            for attr, value in entity:
                if attr != "_id":
                    existing_entity[attr] = value

            stamp = datetime.datetime.utcnow().isoformat()
            existing_entity["modified"] = stamp

            update_result = await self.db[self.entity_type].update_one({"_id": existing_entity["_id"]},
                                                                       {"$set": existing_entity})

            if not update_result.acknowledged:
                return None

            updated_entity = await self.db[self.entity_type].find_one(existing_entity["_id"])

            return updated_entity
        except Exception as e:
            print(e)
