# Testhangfire100reqCase#   T e s t h a n g f i r e 1 0 0 r e q C a s e - L o a d - B a l a n c e r - 
 
 


to test write 

for /l %i in (1,1,100) do curl -X POST http://localhost:5299/api/orders -H "Content-Type: application/json" -d "{\"CustomerName\":\"طلب رقم %i\"}"



to test read 

for /l %i in (1003,1,50) do curl http://localhost:5299/api/orders/%i
