class BankAccount:
    def __init__(self, accountno, name, balance):
        self.__accountno = accountno
        self.__name = name
        self.__balance = balance

    def deposit(self, amount):
        if amount > 0:
            self.__balance += amount
            print(f"Deposited: Rs.{amount}")
        else:
            print("Invalid deposit amount!")

    def withdraw(self, amount):
        if 0 < amount <= self.__balance:
            self.__balance -= amount
            print(f"Withdrawn: Rs.{amount}")
        else:
            print("Insufficient balance or invalid amount!")

    def set_accountno(self, accountno):
        self.__accountno = accountno

    def get_accountno(self):
        return self.__accountno

  
    def set_name(self, name):
        self.__name = name

    def get_name(self):
        return self.__name

    def set_balance(self, balance):
        if balance >= 0:
            self.__balance = balance
        else:
            print("Balance cannot be negative!")

    def get_balance(self):
        return self.__balance



if __name__ == "__main__":
  
    acc = BankAccount(1234567, "Alice", 5000)

    print("Account Number:", acc.get_accountno())
    print("Account Holder Name:", acc.get_name())
    print("Current Balance: Rs.", acc.get_balance())

    acc.deposit(1500)

    acc.withdraw(1000)


    print("Updated Balance: Rs.", acc.get_balance())

   
    acc.set_name("Alice Ashok")
    print("Updated Account Holder Name:", acc.get_name())
