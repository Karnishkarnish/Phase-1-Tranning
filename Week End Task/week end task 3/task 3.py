class Person:
    def __init__(self, name):
        self.name = name

    def __repr__(self):
        return f"Person(name='{self.name}')"

p = Person("Alice")
print(repr(p))  
class MyData:
    def __init__(self, data):
        self.data = data

    def __getitem__(self, index):
        return self.data[index]

obj = MyData(["apple", "banana", "cherry"])
print(obj[1])  

class MyList:
    def __init__(self, items):
        self.items = items

    def __len__(self):
        return len(self.items)

ml = MyList([1, 2, 3])
print(len(ml))  
