$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": false
    });
});
function Delete(id) {
    Swal.fire({
        title: lbTitleMsgDelete,
        text: lbTextMsgDelete,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Accounts/DeleteUser?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkRole,
                lbSuccess
            )
        }
    })
}

Edit = (id,name,image,Email,ActiveUser,Role) => {
    document.getElementById("title").innerHTML = lbTitleEdit;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("userId").value = id;
    document.getElementById("userName").value = name;
    document.getElementById("userEmail").value = Email;
    document.getElementById("image").hidden = false;
    document.getElementById("image").src = "/Images/Users/" + image;
    document.getElementById("imageHide").value = image;
    document.getElementById("ddluserRole").value = Role;
    $('#grPassword').hide();
    $('#grcomPassword').hide();
    var Active = document.getElementById("userActive");
    if (ActiveUser == "True")
        Active.checked = true;
    else Active.checked = false;
    document.getElementById("userPassword").value = "********";
    document.getElementById("userCompare").value = "********";
    document.getElementById("image").hidden = false;


  
    
}


Rest = () => {
    document.getElementById("title").innerHTML = lbAddNewRole;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("userId").value = "";
    document.getElementById("userName").value = "";
    document.getElementById("userEmail").value = "";
    //document.getElementById("userImage").value = "";
    document.getElementById("ddluserRole").value = "";
    document.getElementById("userActive").checked = false;
    $('#grPassword').show();
    $('#grcomPassword').show();
    document.getElementById("userPassword").value = "";
    document.getElementById("userCompare").value = "";
    document.getElementById("image").hidden = true;
    document.getElementById("imgeHide").value = "";


}