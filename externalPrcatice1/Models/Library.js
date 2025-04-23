<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Library Management</title>
</head>

<body>

    <center>

        <form>
            <h2>Hello, ᓚᘏᗢ</h2>
            <input type="number" placeholder="Enter Id" id="Id"><br><br>
            <input type="number" placeholder="Enter BookId" id="BookId"><br><br>
            <input type="text" placeholder="Enter IssuedTo" id="IssuedTo"><br><br>
            <input type="date" placeholder="Enter IssuedDate" id="IssuedDate"><br><br>
            <input type="date" placeholder="Enter ReturnDate" id="ReturnDate"><br><br>
            <button type="button" onclick="insertM()">Insert</button>
            <button type="button" onclick="deleteM()">Delete</button>
            <button type="button" onclick="updateM()">Update</button>
        </form>
        <p style="color: red; font-weight: 700;" id="errorMSG"></p>
        <p style="color: green; font-weight: 700;" id="succMSG"></p>
        <br><br>
        <hr>
        <hr>
        <table id="tbl" border="2">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>BookId</th>
                    <th>IssuedTo</th>
                    <th>IssuedDate</th>
                    <th>ReturnDate</th>
                </tr>
            </thead>
            <tbody id="tbodyMain">
                <tr>
                </tr>
            </tbody>
        </table>
    </center>

    <script>
        const p = document.getElementById('errorMSG');
        const psuc = document.getElementById('succMSG');

        // Function to load data from the API
        const loaddata = () => {
            fetch('http://localhost:51403/api/Issue/GetAll', {
                method: 'GET'
            }).then((res)=>{
                if(!res.ok){
                    res.json().then((data)=>{
                        throw new Error(data.Message || "Internal Error")
                    })
                }
                return res.json()
            }).then((data) => {
                    console.log(data);

                    
                    if(data.length===0){
                        const tbl=document.getElementById('tbl');
                        psuc.style.display="none"
                        p.style.display="block"
                        p.innerText="No Data Found";
                       return tbl.style.display="none"
                    }
                    
                    tbl.style.display="table"
                    const tbody = document.getElementById('tbodyMain');
                    tbody.innerHTML = '';
                    data.forEach(element => {
                        const row = document.createElement('tr');
                        row.innerHTML = `
                            <td>${element.Id}</td>
                            <td>${element.BookID}</td>
                            <td>${element.IssuedTo}</td>
                            <td>${element.IssuedDate}</td>
                            <td>${element.ReturnDate}</td>
                        `;
                        tbody.appendChild(row);
                    });
                })
                .catch((err) => {
                    console.error(err);
                    p.innerText = "Failed to load data.";
                });
        };
        loaddata();

        // Function to insert a new record
        const insertM = () => {
            const BookId = document.getElementById('BookId').value;
            const IssuedTo = document.getElementById('IssuedTo').value;
            const IssuedDate = document.getElementById('IssuedDate').value;
            const ReturnDate = document.getElementById('ReturnDate').value;

            fetch('http://localhost:51403/api/Issue/PostIssue', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    BookID: BookId,
                    IssuedTo: IssuedTo,
                    IssuedDate: IssuedDate,
                    ReturnDate: ReturnDate
                })
            })
                .then((res) => {
                    if (!res.ok) {
                        return res.json().then((err) => {
                            throw new Error(err.Message || "An error occurred");
                        });
                    }
                    return res.json();
                })
                .then((res) => {
                    console.log(res);
                    p.style.display="none"
                    psuc.style.display="block"
                        psuc.innerText = res;
                    loaddata();
                })
                .catch((err) => {
                    console.error(err.message);
                    psuc.style.display="none"
                    p.style.display="block"
                    p.innerText = err.message;
                });
        };

        // Function to delete a record
        const deleteM = () => {
            const Id = document.getElementById('Id').value;

            fetch(`http://localhost:51403/api/Issue/Delete?id=${Id}`, {
                method: 'DELETE'
            })
                .then((res) => {
                    if (!res.ok) {
                        return res.json().then((err) => {
                            throw new Error(err.Message || "An error occurred");
                        });
                    }
                    return res.json();
                })
                .then((res) => {
                    console.log(res);
                    p.style.display="none"
                    psuc.style.display="block"
                    psuc.innerText = "Record deleted successfully!";
                    loaddata();
                })
                .catch((err) => {
                    console.error(err.message);
                    psuc.style.display="none"
                    p.style.display="block"
                    p.innerText = err.message;
                });
        };

        // Function to update a record
        const updateM = () => {
            const Id = document.getElementById('Id').value;
            const BookId = document.getElementById('BookId').value;
            const IssuedTo = document.getElementById('IssuedTo').value;
            const IssuedDate = document.getElementById('IssuedDate').value;
            const ReturnDate = document.getElementById('ReturnDate').value;

            fetch(`http://localhost:51403/api/Issue/PutIssue?id=${Id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    BookID: BookId,
                    IssuedTo: IssuedTo,
                    IssuedDate: IssuedDate,
                    ReturnDate: ReturnDate
                })
            })
                .then((res) => {
                    if (!res.ok) {
                        return res.json().then((err) => {
                            throw new Error(err.Message || "An error occurred");
                        });
                    }
                    return res.json();
                })
                .then((res) => {
                    console.log(res);
                    p.style.display="none"
                    psuc.style.display="block"
                    psuc.innerText = "Record updated successfully!";
                    loaddata();
                })
                .catch((err) => {
                    console.error(err.message);
                    psuc.style.display="none"
                    p.style.display="block"
                    p.innerText = err.message;
                });
        };
    </script>
</body>

</html>
