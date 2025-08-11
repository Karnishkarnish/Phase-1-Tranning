class Stack:
    def __init__(self):
        self.stack = [] 
   
    def push(self, item):
        self.stack.append(item)
        print(f"Pushed: {item}")

   
    def pop(self):
        if not self.is_empty():
            removed = self.stack.pop()
            print(f"Popped: {removed}")
            return removed
        else:
            print("Stack is empty. Cannot pop.")

    def peek(self):
        if not self.is_empty():
            return self.stack[-1]
        else:
            print("Stack is empty.")
            return None

    
    def is_empty(self):
        return len(self.stack) == 0

    def display(self):
        if self.is_empty():
            print("Stack is empty.")
        else:
            print("Stack contents (top to bottom):")
            for item in reversed(self.stack):
                print(item)
if __name__ == "__main__":
    s = Stack()

    s.push(10)
    s.push(20)
    s.push(30)
    
    s.display()

    print("Top element is:", s.peek())

    s.pop()
    s.display()

    print("Is stack empty?", s.is_empty())
