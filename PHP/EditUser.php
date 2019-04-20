<?php
	$con = mysqli_connect('188.121.44.160', 'fmpscores', 'Password1!', 'fmpscores');

	// check for successful connection
	if (mysqli_connect_errno()){
		echo "1: Connection failed";
		exit();
	}
	
	$id = $_POST["id"];
	$name = $_POST["name"];
	$avatar = $_POST["avatar"];
	$colour = $_POST["colour"];
	
	// edit user info
	$query = "UPDATE players SET name = '$name', avatar = '$avatar', colour = '$colour' WHERE id = '$id'";
	mysqli_query($con, $query) or die("2: Failed to edit user");
?>