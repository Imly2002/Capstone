﻿$(document).ready(function () {
    $('#productSearch').select2({
        ajax: {
            url: '/Admin/POS/GetPakyu',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            },
            cache: true
        },
        minimumInputLength: 1,
        templateResult: formatResults
    });


    // Handle the selection event
    $('#productSearch').on('select2:select', function (e) {
        var selectedProduct = e.params.data;

        // Show the selected product name
        $('#selectedProductName').text(selectedProduct.text);

        // Show the input field
        $('#quantityInputDiv').show();

        // Show the modal
        $('#productModal').modal('show');
    });

    function updateTotalAmount() {
        var total = 0;
        $('#tbProduct tbody tr').each(function () {
            var row = $(this);
            var quantity = parseInt(row.find('td:eq(2)').text()); // Assuming the quantity is in the 3rd column
            var retailPrice = parseFloat(row.find('td:eq(3)').text()); // Assuming the retail price is in the 4th column
            total += quantity * retailPrice;
        });
        $('#totalAmountInput').val(total.toFixed(2)); // Display the total with 2 decimal places
    }

    $('#addProductButton').click(function () {
        var quantity = $('#quantity').val();
        var selectedProductName = $('#selectedProductName').text();

        if (quantity && selectedProductName) {
            // Check if the quantity exceeds the current stock
            $.ajax({
                url: '/Admin/POS/CheckStock',
                method: 'GET',
                data: { productName: selectedProductName, quantity: quantity },
                success: function (isStockAvailable) {
                    if (isStockAvailable) {
                        // Continue with the rest of your existing code
                        $.ajax({
                            url: '/Admin/POS/GetRetailPrice',
                            method: 'GET',
                            data: { productName: selectedProductName },
                            success: function (retailPrice) {
                                if (retailPrice !== null) {
                                    // Calculate the total
                                    var total = quantity * retailPrice;

                                    var newRow = $('<tr>');
                                    newRow.append($('<td><button type="button" class="btn btn-danger btn-sm btn-remove">Remove</button></td>'));
                                    newRow.append($('<td>' + selectedProductName + '</td>'));
                                    newRow.append($('<td>' + quantity + '</td>'));
                                    newRow.append($('<td>' + retailPrice + '</td>'));
                                    newRow.append($('<td>' + total + '</td>'));

                                    $('#tbProduct tbody').append(newRow);

                                    $('#productModal').modal('hide');

                                    updateTotalAmount(); // Update the total amount
                                } else {
                                    alert('Retail price not available for the selected product. Please check your product data.');
                                }
                            },
                            error: function () {
                                alert('An error occurred while fetching the retail price. Please try again later.');
                            }
                        });

                    } else {
                        alert('Insufficient stock for the selected product. Please adjust the quantity.');
                    }
                },
                error: function () {
                    alert('An error occurred while checking stock. Please try again later.');
                }
            });
        }
    });

    $('#qtyclose').on('click', function () {
        $('#productModal').modal('hide');
    });

    // Handle the "Remove" button click event using event delegation
    $('#tbProduct').on('click', '.btn-remove', function () {
        $(this).closest('tr').remove();
        updateTotalAmount(); // Update the total amount
    });

    //charge modal
    $('#openChargeModalButton').on('click', function () {
        $('#chargeModal').modal('show');
    });

    $('#addChargeButton').on('click', function () {
        
        var totalCharge = parseFloat(document.getElementById("total-charge-display").value) || 0;

        var currentTotalAmount = parseFloat(document.getElementById("totalAmountInput").value) || 0;

        var newTotalAmount = currentTotalAmount + totalCharge;

        document.getElementById("totalAmountInput").value = newTotalAmount.toFixed(2);


        $('#chargeModal').modal('hide');
    });

    $('#closeModal').on('click', function () {
        $('#chargeModal').modal('hide');
    });


    //bank transfer
    $('#openbankmodal').on('click', function () {
        $('#bankModal').modal('show');

        totalAmount = parseFloat($('#totalAmountInput').val()) || 0;
        $('#totalAmountinBank').val(totalAmount.toFixed(2));
    });

    $('#closebankmodal').on('click', function () {
        $('#bankModal').modal('hide');
    });


    //cash payment
    $('#opencashmodal').on('click', function () {
        $('#cashModal').modal('show');

        totalAmount = parseFloat($('#totalAmountInput').val()) || 0;
        $('#total-amounttopay-display').val(totalAmount.toFixed(2));
    });

    $('#closecashmodal').on('click', function () {
        $('#cashModal').modal('hide');
    });


    //discount modal
    $('#addDiscount').on('click', function () {
        $('#discountModal').modal('show');
    });

    $('#applyDiscountButton').on('click', function () {
        let discountValue = 0;

        const totalAmountInput = document.getElementById("totalAmountInput");
        const discountinputdisplay = document.getElementById("discountinputdisplay");

        if (percentRadio.checked) {
            const percent = parseFloat(percentInput.value) || 0;
            discountValue = (percent / 100) * parseFloat(totalAmountInput.value) || 0;
        } else if (amountRadio.checked) {
            discountValue = parseFloat(amountInput.value) || 0;
        }
        let totalAmount = parseFloat(totalAmountInput.value) || 0;

        totalAmount -= discountValue;

        totalAmountInput.value = totalAmount.toFixed(2); 

        discountinputdisplay.value = discountValue.toFixed(2);

        $('#discountModal').modal('hide');
    });
    $('#closeDiscount').on('click', function () {
        $('#discountModal').modal('hide');
    });

    //discount remove
    $('#removeDiscountBtn').on('click', function () {
        var totalAmount = parseFloat($('#totalAmountInput').val()) || 0;
        var discountValue = parseFloat($('#discountinputdisplay').val()) || 0;

        totalAmount += discountValue;

        $('#totalAmountInput').val(totalAmount.toFixed(2));

        $('#discountinputdisplay').val("0.00");
    });

    //charge remove
    $('#removeChargeBtn').on('click', function () {
        // Assuming you have a variable to store the current total amount
        var totalAmount = parseFloat($('#totalAmountInput').val()) || 0;

        // Assuming you have a variable to store the current charge value
        var totalCharge = parseFloat($('#chargeinputdisplay').val()) || 0;

        // Subtract the charge from the total amount
        totalAmount -= totalCharge;

        // Update the total amount on the front-end
        $('#totalAmountInput').val(totalAmount.toFixed(2));

        // Reset the charge display
        $('#chargeinputdisplay').val("0.00");
    });

    $('#newSaleButton').click(function () {
        // Clear customer information input fields
        $('#firstName').val('');
        $('#lastName').val('');
        $('#emailAdd').val('');
        $('#contactNum').val('');
        $('#houseNum').val('');
        $('#streetName').val('');
        $('#brgy').val('');
        $('#cityName').val('');
        $('#provinceName').val('');
        $('#zipCode').val('');

        // Clear the product search and table
        $('#productSearch').val('');
        $('#tbProduct tbody').empty();

        // Clear the details and total
        $('#totalAmountInput').val('0');
        $('#discountinputdisplay').val('');
        $('#chargeinputdisplay').val('');
        $('#tenderedamountInput').val('');
        $('#changeInput').val('');

        // Reset the selected product name in the modal
        $('#selectedProductName').text('');

        // Close any open modals
        $('#productModal').modal('hide');
        $('#discountModal').modal('hide');
        $('#chargeModal').modal('hide');
        $('#cashModal').modal('hide');
        $('#bankModal').modal('hide');
    });

    $("#holdTransactionBtn").click(function () {
        var orderId = 45888;

        $.ajax({
            url: "/Admin/POS/StartProcessing", // Make sure to start the URL with a "/"
            type: "POST",
            data: { orderId: orderId },
            success: function (data) {
                alert('ge');
            },
            error: function (error) {
                alert('de');
                console.log(error); // Log the error to the console for more details
            }
        });
    });

    updateTotalAmount();

});


function formatResults(data) {
    if (data.loading)
        return data.text;

    var imageUrl = data.img; // Assuming data.img contains the relative image path from the wwwroot folder

    // Construct the complete image URL by prepending the application base URL
    var completeImageUrl = window.location.origin + imageUrl;


    var container = $(
        `<table width="100%">
            <tr>
                <td style="width:60px">
                    <img style="height:60px;width:60px;margin-right:10px" src="${completeImageUrl}"/>
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.text}</p>
                    <p style="margin:2px">${data.retailPrice}</p>
                </td>
            </tr>
         </table>`
    );

    return container;
}


$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});

//discount
const percentRadio = document.getElementById("percentRadio");
const amountRadio = document.getElementById("amountRadio");
const percentInput = document.getElementById("percentInput");
const amountInput = document.getElementById("amountInput");
percentRadio.addEventListener("change", function () {
    if (percentRadio.checked) {
        percentInput.disabled = false;
        amountInput.disabled = true;
    }
});

amountRadio.addEventListener("change", function () {
    if (amountRadio.checked) {
        amountInput.disabled = false;
        percentInput.disabled = true;
    }
});


function calculateTotalChange() {
    const ctenderedamount = parseFloat(document.getElementById("tenderedamountInput").value) || 0;
    const totalcChange = ctenderedamount - totalAmount;

    document.getElementById("changeInput").value = totalcChange.toFixed(2);
}

function calculateTotalBank() {
    const btenderedamount = parseFloat(document.getElementById("banktenderedamount").value) || 0;
    const totalbChange = btenderedamount - totalAmount;

    document.getElementById("bankchange").value = totalbChange.toFixed(2);
}

//charge modal parin pero calculations
function calculateTotalCharge() {
    const serviceFee = parseFloat(document.getElementById("service-fee-input").value) || 0;
    const deliveryFee = parseFloat(document.getElementById("delivery-fee-input").value) || 0;
    const totalCharge = serviceFee + deliveryFee;

    document.getElementById("total-charge-display").value = totalCharge.toFixed(2);

    document.getElementById("chargeinputdisplay").value = totalCharge.toFixed(2);
}

function showCancelConfirmation() {
    var confirmation = window.confirm('Are you sure you want to cancel this transaction? This action cannot be undone!');

    if (confirmation) {
        alert('Transaction successfully canceled');
    } else {
        alert('Action canceled.');
    }
}

function showHoldConfirmation() {
    var confirmation = window.confirm('Are you sure you want to put this transaction on hold?');

    if (confirmation) {
        alert('If you want to complete this transaction, go to "Manage Order" module under Sales.');
    } else {
        alert('Action canceled.');
    } 
} 