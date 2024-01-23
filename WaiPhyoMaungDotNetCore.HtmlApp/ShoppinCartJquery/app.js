$(document).ready(function () {
    // Define jQuery objects for various HTML elements
    let $listProductHTML = $(".listProduct");
    let $listCartHTML = $(".listCart");
    let $iconCart = $(".icon-cart");
    let $iconCartSpan = $(".icon-cart span");
    let $body = $("body");
    let $closeCart = $(".close");
    let products = [];
    let cart = [];

    // Event listener to toggle the visibility of the shopping cart
    $iconCart.click(function () {
        $body.toggleClass("showCart");
    });

    // Event listener to close the shopping cart
    $closeCart.click(function () {
        $body.toggleClass("showCart");
    });

    // Function to add product data to the HTML
    const addDataToHTML = () => {
        // add new datas
        if (products.length > 0) {
            // if has data
            console.log(products);
            products.forEach((product) => {
                // Create a new product element
                let $newProduct = $("<div>", {
                    "data-id": product.id,
                    class: "item",
                    html: `<img src="${product.image}" alt="">
                           <h2>${product.name}</h2>
                           <div class="price">$${product.price}</div>
                           <button class="addCart">Add To Cart</button>`,
                });

                // Append the new product to the product list
                $listProductHTML.append($newProduct);
            });
        }
    };

    $listProductHTML.on("click", ".addCart", function () {
        let $positionClick = $(this).closest(".item");
        let id_product = $positionClick.data("id");
        addToCart(id_product);
        showNotification("Added to Cart", "Product has been added to the cart.", "success", 1500);
    
        // Wait for the image to load, then initialize exzoom
        $positionClick.find('img').on('load', function() {
            initializeExzoom($(this));
        });
    });
    
    
    const initializeExzoom = ($productElement) => {
        // Find the img element within the product element
        let $image = $productElement.find('img');

        // Check if the image element exists
        if ($image.length > 0) {
            // Initialize exzoom only if the image element is found
            $image.parent().exzoom({
                "autoPlay": false,
            });
        } else {
            console.error("Image element not found within the product element.");
        }
    };

    

    // Function to add a product to the shopping cart
    const addToCart = (product_id) => {
        let positionThisProductInCart = cart.findIndex(
            (value) => value.product_id == product_id
        );
        if (cart.length <= 0) {
            cart = [
                {
                    product_id: product_id,
                    quantity: 1,
                },
            ];
        } else if (positionThisProductInCart < 0) {
            cart.push({
                product_id: product_id,
                quantity: 1,
            });
        } else {
            cart[positionThisProductInCart].quantity =
                cart[positionThisProductInCart].quantity + 1;
        }
        addCartToHTML();
        addCartToMemory();
    };

    // Function to save the shopping cart to local storage
    const addCartToMemory = () => {
        localStorage.setItem("cart", JSON.stringify(cart));
    };

    // Function to update the shopping cart in the HTML
    const addCartToHTML = () => {
        $listCartHTML.empty();
        let totalQuantity = 0;
        if (cart.length > 0) {
            cart.forEach((item) => {
                totalQuantity = totalQuantity + item.quantity;
                // Create a new cart item element
                let $newItem = $("<div>", {
                    class: "item",
                    "data-id": item.product_id,
                    html: `<div class="image">
                               <img src="${products.find(
                                   (p) => p.id == item.product_id
                               ).image}">
                           </div>
                           <div class="name">${
                               products.find((p) => p.id == item.product_id).name
                           }</div>
                           <div class="totalPrice">$${
                               products.find((p) => p.id == item.product_id).price *
                               item.quantity
                           }</div>
                           <div class="quantity">
                               <span class="minus"><</span>
                               <span>${item.quantity}</span>
                               <span class="plus">></span>
                           </div>`,
                });

                // Append the new cart item to the shopping cart
                $listCartHTML.append($newItem);
            });
        }
        // Update the total quantity in the cart icon
        $iconCartSpan.text(totalQuantity);
    };

    // Event listener for changing the quantity of items in the shopping cart
    $listCartHTML.on("click", ".minus, .plus", function () {
        let $positionClick = $(this).closest(".item");
        let product_id = $positionClick.data("id");
        let type = $(this).hasClass("plus") ? "plus" : "minus";
        changeQuantityCart(product_id, type);
        showToast(type === "plus" ? "Quantity increased" : "Quantity decreased", "success");
    });

    
    // Function to change the quantity of items in the shopping cart
    const changeQuantityCart = (product_id, type) => {
        let positionItemInCart = cart.findIndex(
            (value) => value.product_id == product_id
        );
        if (positionItemInCart >= 0) {
            let info = cart[positionItemInCart];
            switch (type) {
                case "plus":
                    cart[positionItemInCart].quantity =
                        cart[positionItemInCart].quantity + 1;
                    break;

                default:
                    let changeQuantity = cart[positionItemInCart].quantity - 1;
                    if (changeQuantity > 0) {
                        cart[positionItemInCart].quantity = changeQuantity;
                    } else {
                        cart.splice(positionItemInCart, 1);
                    }
                    break;
            }
        }
        addCartToHTML();
        addCartToMemory();
    };

    // Function to initialize the application
    const initApp = () => {
        // get data product
        $.getJSON("products.json", function (data) {
            products = data;
            addDataToHTML();

            // get data cart from memory
            if (localStorage.getItem("cart")) {
                cart = JSON.parse(localStorage.getItem("cart"));
                addCartToHTML();
            }
        });
    };

    // Initialize the application
    initApp();
     // Initialize exzoom for the clicked product image
    
     

// // Function to initialize exzoom for a specific product element
// const initializeExzoom = ($productElement) => {
//     // Find the img element within the product element
//     let $image = $productElement.find('img');

//     // Check if the image element exists
//     if ($image.length > 0) {
//         // Initialize exzoom only if the image element is found
//         $image.parent().exzoom({
//             "autoPlay": false,
//         });
//     } else {
//         console.error("Image element not found within the product element.");
//     }
// };

});
