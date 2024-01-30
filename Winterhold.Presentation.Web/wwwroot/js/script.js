
(() => {
    addButtonSummaryListener();
    addCategoryInsertButtonListener();
    addUpdateCategoryButtonListener()
    addUpsertCategoryButtonListener();
    addCloseButtonListener();
    addMembershipEventListener();
    addLoanInsertButtonListener();
    addLoanSubmitUpsertButtonListener();
    addUpdateLoanButtonListener(); 
    addLoanDetailButtonListener();
})();
function showConfirmation(controller, action, deleteId) {
    let isConfirm = confirm(`Apakah anda yakin ingin menghapus data dengan id ${deleteId} ?`);
    if (isConfirm) {
        let actionUrl = `${controller}/${action}/${deleteId}`;
        window.location.href = actionUrl;
        
    }
    return false;
}
function writeValidationMessage(errorMessages) {
    console.log(errorMessages);
    for (let error in errorMessages.errors) {
        let field = error;
        let message = errorMessages.errors[error];
        document.querySelector(`.modal-layer .upsert-form [validation-for="${field}"]`).textContent = message[0];
    }
}
function addButtonSummaryListener() {
    let button = document.querySelectorAll('.summary-button');
    if (button != null) {
        for (let btn of button) {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                let modal = document.querySelector('.modal-layer');
                modal.classList.add('modal-layer--opened');
                let dialog = document.querySelector('.popup-dialog div');
                dialog.innerHTML = "<b>Book Summary : </b>" + btn.getAttribute('data-summary');
            })
        }
    }
}
function addCloseButtonListener() {
    let buttonClose = document.querySelectorAll('.close-button');
    if (buttonClose != null) {
        for (let button of buttonClose) {
            button.addEventListener('click', () => {
                let modal = document.querySelector('.modal-layer');
                modal.classList.remove('modal-layer--opened');
                let inputTag = document.querySelectorAll('.popup-dialog .upsert-form input');
                for (let input of inputTag) {
                    if (input.getAttribute("type") != 'submit') {
                        input.value = null;
                    }
                }
            });
        }
    }
}
function addCategoryInsertButtonListener() {
    let button = document.querySelector('#category-insert-button');
    if (button != null) {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            let modal = document.querySelector('.modal-layer');
            modal.classList.add('modal-layer--opened');
        }); 
        
    }
}
function addUpdateCategoryButtonListener() {
    let button = document.querySelectorAll('#category-update-button');
    if (button != null) {
        for (let btn of button) {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                
                let modal = document.querySelector('.modal-layer');
                modal.classList.add('modal-layer--opened');
                let name = document.querySelector('#category-upsert-form #Name');
                name.value = btn.getAttribute('data-name');
                name.setAttribute("readonly", "readonly");
                document.querySelector('#category-upsert-form [type="submit"]').classList.add('submit-update');

                //get data by category name
                const request = new XMLHttpRequest();
                request.open(`GET`, `http://localhost:5201/api/BookCategory/${name.value}`);

                request.send();

                request.onload = () => {
                    let result = JSON.parse(request.response);
                    console.log(result);
                    document.querySelector('#category-upsert-form #Floor').value = result.floor;
                    document.querySelector('#category-upsert-form #Isle').value = result.isle;
                    document.querySelector('#category-upsert-form #Bay').value = result.bay;

                };
                
            }); 
        }
    }
}
function addUpsertCategoryButtonListener() {
    
    let submitButton = document.querySelector('#category-upsert-form [type="submit"]');
    if (submitButton != null) {
        submitButton.addEventListener('click', (e) => {
            e.preventDefault();
            let data = {
                name: document.querySelector('#category-upsert-form #Name').value == "" ? null : document.querySelector('#category-upsert-form #Name').value, 
                floor: document.querySelector('#category-upsert-form #Floor').value,
                isle: document.querySelector('#category-upsert-form #Isle').value == "" ? null : document.querySelector('#category-upsert-form #Isle').value,
                bay: document.querySelector('#category-upsert-form #Bay').value == "" ? null : document.querySelector('#category-upsert-form #Bay').value
            };
            let requestMethod = submitButton.classList.contains("submit-update") ? "PUT" : "POST";
            //let requestMethod = document.querySelector('#category-upsert-form #Name').getAttribute('readonly') != null ? "PUT" : "POST";
            let url = "http://localhost:5201/api/BookCategory";

            const request = new XMLHttpRequest();
            request.open(requestMethod, url);
            request.setRequestHeader('Content-type', 'application/json');
            request.send(JSON.stringify(data));

            request.onload = () => {
                if (request.status == 200) {
                    location.reload();
                }
                else if (request.status == 422) {
                    document.querySelector(`.modal-layer .upsert-form [validation-for="Name"]`).textContent = request.response;
                }
                else {
                    let result = JSON.parse(request.response);
                    console.log(result);
                    writeValidationMessage(result);
                }
            }; 
            
        });
    }
}

function addMembershipEventListener() {
    let td = document.querySelectorAll('.membership-detail'); 
    if (td != null) {
        for (let d of td) {
            d.addEventListener('click', (e) => {
                e.preventDefault();
                let modal = document.querySelector('.modal-layer');
                modal.classList.add('modal-layer--opened');

                const request = new XMLHttpRequest();
                request.open("GET", `http://localhost:5201/api/Customer/${d.textContent}`);
                request.setRequestHeader('Content-type', 'application/json');
                request.send();

                request.onload = () => {
                    let result = JSON.parse(request.response);
                    console.log(result);
                    let dialog = document.querySelector('.popup-dialog tbody');
                    result.lastName = result.lastName == null ? "" : result.lastName;
                    result.phone = result.phone == null ? "-" : result.phone;
                    result.address = result.address == null ? "-" : result.address;
                    result.birthDate = new Date(result.birthDate).toISOString().split('T')[0];
                    dialog.innerHTML =
                        `<tr>
                            <td>Membership Number</td>
                            <td>${result.membershipNumber}</td>
                        </tr>
                        <tr>
                            <td>Full Name</td>
                            <td>${result.firstName} ${result.lastName}</td>
                        </tr>
                        <tr>
                            <td>Birth Date</td>
                            <td>${result.birthDate}</td>
                        </tr>
                        <tr>
                            <td>Gender</td>
                            <td>${result.gender}</td>
                        </tr>
                        <tr>
                            <td>Phone</td>
                            <td>${result.phone}</td>
                        </tr>
                        <tr>
                            <td>Address</td>
                            <td>${result.address}</td>
                        </tr>`;
                }
            })
        }
    }
}
function addLoanInsertButtonListener() {
    let button = document.querySelector('#loan-insert-button');
    if (button != null) {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            let modal = document.querySelector('.modal-layer');
            modal.classList.add('modal-layer--opened');

            let detailDialog = document.querySelector(".popup-dialog .loan-detail");
            detailDialog.classList.add('display-none');
            detailDialog.querySelector('div').classList.remove('flex-row-layer--opened');
            detailDialog.classList.remove('modal-layer--opened');

            let formDialog = document.querySelector(".popup-dialog .upsert-form");
            formDialog.classList.add("modal-layer--opened");
            formDialog.classList.remove('display-none');

            const request = new XMLHttpRequest();
            request.open("GET", `http://localhost:5201/api/Loan/Insert`);
            request.setRequestHeader('Content-type', 'application/json');
            request.send();

            request.onload = () => {
                let result = JSON.parse(request.response);
                console.log(result);

                let customerDropdown = document.querySelector('#CustomerNumber');
                customerDropdown.innerHTML = '<option value="">Select Customer</option>';
                for (let customer of result.customers) {
                    customerDropdown.innerHTML += `<option value="${customer.value}">${customer.text}</option>`;
                }
                let bookDropdown = document.querySelector('#BookCode');
                bookDropdown.innerHTML = '<option value="">Select Book</option>';
                for (let book of result.books) {
                    bookDropdown.innerHTML += `<option value="${book.value}">${book.text}</option>`;
                }
            }
        });

    }
}
function addLoanSubmitUpsertButtonListener() {
    let button = document.querySelector('#loan-upsert-form [type="submit"]');
    if (button != null) {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            let requestMethod = "POST";
            let url = "http://localhost:5201/api/Loan";
            let dataId = 0;

            let upsertType = button.classList.contains("submit-update") ;
            if (upsertType) {
                requestMethod = "PUT";
                dataId = document.querySelector('#loan-upsert-form #Id').value;
            }

            let data = {
                id: dataId,
                customerNumber: document.querySelector('#loan-upsert-form #CustomerNumber').value,
                bookCode: document.querySelector('#loan-upsert-form #BookCode').value,
                loanDate: document.querySelector('#loan-upsert-form #LoanDate').value,
                note: document.querySelector('#loan-upsert-form #Note').value == "" ? null : document.querySelector('#loan-upsert-form #Note').value
            };

            const request = new XMLHttpRequest();
            request.open(requestMethod, url);
            request.setRequestHeader('Content-type', 'application/json');
            request.send(JSON.stringify(data));

            request.onload = () => {
                if (request.status == 200) {
                    location.reload();
                }
                else {
                    let result = JSON.parse(request.responseText);
                    console.log(result);
                    writeValidationMessage(result);
                }
            }; 
        })
    }
}
function addUpdateLoanButtonListener() {
    let button = document.querySelectorAll('.update-loan-button');
    if (button != null) {
        for (let btn of button) {
            btn.addEventListener('click', (e) => {
                e.preventDefault();

                let modal = document.querySelector('.modal-layer');
                modal.classList.add('modal-layer--opened');

                let detailDialog = document.querySelector(".popup-dialog .loan-detail");
                detailDialog.querySelector('div').classList.remove('flex-row-layer--opened');
                detailDialog.classList.add('display-none');
                detailDialog.classList.remove('modal-layer--opened');

                let formDialog = document.querySelector(".popup-dialog .upsert-form");
                formDialog.classList.add("modal-layer--opened"); 
                formDialog.classList.remove('display-none');
                
                document.querySelector('#loan-upsert-form [type="submit"]').classList.add('submit-update');
                let loanId = document.querySelector('#loan-upsert-form #Id');
                loanId.value = btn.getAttribute('data-id');

                //get data by category name
                const request = new XMLHttpRequest();
                request.open(`GET`, `http://localhost:5201/api/Loan/Update/${loanId.value}`);

                request.send();

                request.onload = () => {
                    let result = JSON.parse(request.response);
                    console.log(result);

                    document.querySelector('#loan-upsert-form #LoanDate').value = new Date(result.loan.loanDate).toISOString().split('T')[0];
                    document.querySelector('#loan-upsert-form #Note').value = result.loan.note;

                    let customerDropdown = document.querySelector('#CustomerNumber');
                    customerDropdown.innerHTML = `<option value="${result.loan.customerNumber}">${result.customerName}</option>`;
                    for (let customer of result.customers) {
                        customerDropdown.innerHTML += `<option value="${customer.value}">${customer.text}</option>`;
                    }
                    let bookDropdown = document.querySelector('#BookCode');
                    bookDropdown.innerHTML = `<option value="${result.loan.bookCode}">${result.bookTitle}</option>`;
                    for (let book of result.books) {
                        bookDropdown.innerHTML += `<option value="${book.value}">${book.text}</option>`;
                    }
                };
            });
        }
    }
}
function addLoanDetailButtonListener() {
    let button = document.querySelectorAll('.detail-loan-button');
    if (button != null) {
        for (let btn of button) {
            btn.addEventListener('click', (e) => {
                e.preventDefault();

                let modal = document.querySelector('.modal-layer');
                modal.classList.add('modal-layer--opened');

                let detailDialog = document.querySelector(".popup-dialog .loan-detail");
                detailDialog.classList.add('modal-layer--opened');
                detailDialog.classList.remove('display-none');
                detailDialog.querySelector('div').classList.add('flex-row-layer--opened');

                let formDialog = document.querySelector(".popup-dialog .upsert-form");
                formDialog.classList.add("display-none");
                formDialog.classList.remove('modal-layer--opened');

                let loanId = btn.getAttribute('data-id');

                //get data by category name
                const request = new XMLHttpRequest();
                request.open(`GET`, `http://localhost:5201/api/Loan/Detail/${loanId}`);
                request.send();

                request.onload = () => {
                    let result = JSON.parse(request.response);
                    
                    result.membershipExpireDate = new Date(result.membershipExpireDate).toISOString().split('T')[0];
                    let customerBox = document.querySelector('.popup-dialog .loan-customer-detail tbody');
                    customerBox.innerHTML = `
                            <tr>
                                <td>Membership Number</td>
                                <td>${result.membershipNumber}</td>
                            </tr>
                            <tr>
                                <td>Full Name</td>
                                <td>${result.memberFullName}</td>
                            </tr>
                            <tr>
                                <td>Phone</td>
                                <td>${result.memberPhone}</td>
                            </tr>
                            <tr>
                                <td>Membership Expire Date</td>
                                <td>${result.membershipExpireDate}</td>
                            </tr>`;

                    let bookBox = document.querySelector('.popup-dialog .loan-book-detail tbody');
                    bookBox.innerHTML = `
                            <tr>
                                <td>Title</td>
                                <td>${result.bookTitle}</td>
                            </tr>
                            <tr>
                                <td>Category</td>
                                <td>${result.bookCategory}</td>
                            </tr>
                            <tr>
                                <td>Author</td>
                                <td>${result.bookAuthorFullName}</td>
                            </tr>
                            <tr>
                                <td>Floor</td>
                                <td>${result.categoryFloor}</td>
                            </tr>
                            <tr>
                                <td>Isle</td>
                                <td>${result.categoryIsle}</td>
                            </tr>
                            <tr>
                                <td>Bay</td>
                                <td>${result.categoryBay}</td>
                            </tr>`;
                };
            });
        }
    }
}