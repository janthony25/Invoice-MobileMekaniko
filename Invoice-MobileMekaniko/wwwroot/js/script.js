document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('addItemButton').addEventListener('click', function () {
        // Create new invoice item container
        var newInvoiceItem = document.createElement('div');
        newInvoiceItem.className = 'invoice-item d-flex mb-3';

        // Calculate the new index for the new invoice item
        var index = document.querySelectorAll('.invoice-item').length;

        // Create new form group for Item Name
        var newItemNameGroup = document.createElement('div');
        newItemNameGroup.className = 'form-group';
        newItemNameGroup.innerHTML = `
            <label>Item Name</label>
            <input type="text" name="InvoiceItems[${index}].ItemName" class="form-control input" />
            <span class="text-danger"></span>
        `;

        // Create new form group for Quantity
        var newQuantityGroup = document.createElement('div');
        newQuantityGroup.className = 'form-group ms-4';
        newQuantityGroup.innerHTML = `
            <label>Quantity</label>
            <input type="number" name="InvoiceItems[${index}].Quantity" class="form-control" />
            <span class="text-danger"></span>
        `;

        // Create new form group for Item Price
        var newItemPriceGroup = document.createElement('div');
        newItemPriceGroup.className = 'form-group ms-4';
        newItemPriceGroup.innerHTML = `
            <label>Item Price</label>
            <input type="number" name="InvoiceItems[${index}].ItemPrice" class="form-control" />
            <span class="text-danger"></span>
        `;

        // Append form groups to the new invoice item container
        newInvoiceItem.appendChild(newItemNameGroup);
        newInvoiceItem.appendChild(newQuantityGroup);
        newInvoiceItem.appendChild(newItemPriceGroup);

        // Append the new invoice item container to the invoice items container
        document.getElementById('invoiceItems').appendChild(newInvoiceItem);
    });
});