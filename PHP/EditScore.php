<?php
	$con = mysqli_connect('188.121.44.160', 'fmpscores', 'Password1!', 'fmpscores');

	// check for successful connection
	if (mysqli_connect_errno()){
		echo "1: Connection failed";
		exit();
	}
	
	$id = $_POST["id"];
	$score = $_POST["score"];
	
	// edit score
	$query = "UPDATE scores SET score = '$score' WHERE user_id = '$id'";
	mysqli_query($con, $query) or die("2: Failed to create user");
	echo "0";
?>