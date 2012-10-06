from TestConsoleApp.PythonScripting import *

for line in salesBasket.Lines:

    if line.ProductName == 'Prod1':
        discount = line.Amount * 0.2
        line.Amount = line.Amount - discount
        print 'product: ' + line.ProductName + ', discount given: ' + discount.ToString()

    if line.Quantity >= 10:
        line.Amount = line.Amount - line.ProductPrice
        print 'product: ' + line.ProductName + ', discount given: ' + line.ProductPrice.ToString()
