from core.repos.base_repo import BaseRepo


class JournalRepo(BaseRepo):
    def __init__(self):
        super().__init__("journal_entries")
