﻿using QuasarShopData;

namespace QuasarShop.Models;

public class NavbarViewModel
{
    public IEnumerable<Catalog> Catalogs { get; set; }
    public int? FavoriteCount { get; set; } = null;
    public int? ShoppingCartItemCount { get; set; } = null;
}
