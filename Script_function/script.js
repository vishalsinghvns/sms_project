<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

$(document).ready(function () {
    $("#deleteSelectedUsers1").click(function () {
        var selectedIds = $(".user-checkbox:checked").map(function () {
            return $(this).val();
        }).get();

        if (selectedIds.length > 0) {
            $.ajax({
                url: "/UserMaster/SoftDeleteBulkUsers",
                type: "POST",
                data: { userIds: selectedIds }, // Ensure it’s sent as an array
                beforeSend: function () {
                    $("#deleteSelectedUsers").prop("disabled", true); // Disable button to prevent multiple clicks
                },
                success: function (response) {
                    if (response.success === true) {
                        alert("Users marked as deleted!");
                        location.reload(); // Reload to reflect changes
                    } else {
                        alert("Error updating users.");
                    }
                },
                error: function (xhr, status, error) {
                    alert("An error occurred: " + error); // Handle unexpected errors
                },
                complete: function () {
                    $("#deleteSelectedUsers").prop("disabled", false); // Re-enable button after request
                }
            });
        } else {
            alert("Please select at least one user.");
        }
    });
});

    $(document).ready(function () {
        $("#deleteSelectedUsers").click(function () {
            var selectedIds = $(".user-checkbox:checked").map(function () {
                return $(this).val();
            }).get().join(",");

            if (selectedIds.length > 0) {
                $.ajax({
                    url: "/UserMaster/SoftDeleteBulkUsers",
                    type: "POST",
                    data: { userIds: selectedIds },
                    success: function (response) {
                        if (response.success==true) {
                            alert("Error updating users.");
                        } else {
                            alert("Users marked as deleted!");
                            location.reload();
                        }
                    }
                });
            } else {
                alert("Please select at least one user.");
            }
        });
    });

    $(document).ready(function () {
        // Function to enable/disable delete button
        function toggleDeleteButton() {
            if ($(".user-checkbox:checked").length > 0) {
                $("#deleteSelectedUsers").prop("disabled", false).css({ "border-color": "red", "cursor": "pointer", "background": "red", "border": "1px solid red","color":"#ffffff","border-radius":"5px" });
            } else {
                $("#deleteSelectedUsers").css({ "border-color": "gray", "cursor": "not-allowed", "background": "gray", "border": "1px solid gray", "color": "black", "border-radius": "0px"}).prop("disabled", true);
            }
        }

        // Event listener for checkbox changes
        $(".user-checkbox").change(function () {
            toggleDeleteButton();
        });

        // Initial check in case of pre-selected items
        toggleDeleteButton();
    });


$(document).ready(function () {
        // Toggle checkboxes when "Select All" is clicked
        $("#selectAll").change(function () {
            $(".user-checkbox").prop("checked", $(this).prop("checked"));
            toggleDeleteButton();
        });

        // Enable/disable delete button based on checkbox selection
        $(".user-checkbox").change(function () {
            toggleDeleteButton();

            // If all checkboxes are checked, check "Select All" checkbox
            if ($(".user-checkbox:checked").length === $(".user-checkbox").length) {
                $("#selectAll").prop("checked", true).css({ "border-color": "gray", "cursor": "not-allowed", "background": "gray", "border": "1px solid gray", "color": "black", "border-radius": "0px" });
            } else {
                $("#selectAll").prop("checked", false).css({ "border-color": "red", "cursor": "pointer", "background": "red", "border": "1px solid red", "color": "#ffffff", "border-radius": "5px" });
            }
        });

        // Function to enable/disable delete button
        function toggleDeleteButton() {
            if ($(".user-checkbox:checked").length > 0) {
                $("#deleteSelectedUsers").prop("disabled", false).css({ "border-color": "red", "cursor": "pointer", "background": "red", "border": "1px solid red", "color": "#ffffff", "border-radius": "5px" });
            } else {
                $("#deleteSelectedUsers").prop("disabled", true).css({ "border-color": "gray", "cursor": "not-allowed", "background": "gray", "border": "1px solid gray", "color": "black", "border-radius": "0px" });
            }
        }
    });