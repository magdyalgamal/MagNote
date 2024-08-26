UPDATE Sales_Invoices 
SET     Invoice_Customer_No = (SELECT  Customers.Customer_No 
                          FROM   Customers 
                          WHERE  Customers.Customer_Name = 
                                                Sales_Invoices.Invoice_Customer_Name) 