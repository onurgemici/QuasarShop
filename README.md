# QuasarShop
E-Ticaret uygulamasıdır
- Katalog
- Ürün
- Sipariş
- Ödeme
- Üyelik

Özellikleri bulunur.
# Veri Modeli
- Catalog
  - Id
  - Name
    
- Product
  - Id
  - Name
  - Desc
  - Price
  - DiscountRate
  - Image
  
- User
- Role
- CarouselImage
  - Id
  - Image
  - StartDate
  - EndDate
 
- ShoppingCart
  - Id
  - UserId
  - ProductId
  - Quantity
    
- Order
  - Id
  - Date
  - UserId
  - CargoTrackingkNumber
    
- OrderDetail
  - Id
  - ProductId
  - Price
  - DiscountRate
  - Quantity
    
- UserAddress
  - Id
  - UserId
  - Name
  - Text
    
- ProductImages
  - Id
  - Image
  - ProductId
    
- Comments
  - Id
  - UserId
  - Text
  - Date
  - PrductId
  - Enabled
  - Rate
    
