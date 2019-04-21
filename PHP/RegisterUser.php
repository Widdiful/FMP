<?php
	$con = mysqli_connect('188.121.44.160', 'fmpscores', 'Password1!', 'fmpscores');
	
	// check for successful connection
	if (mysqli_connect_errno()){
		echo "1: Connection failed";
		exit();
	}
	
	// add new user
	$query = "INSERT INTO players (name, avatar, colour) VALUES ('New player', '0', 'FFFFFF')";
	mysqli_query($con, $query) or die("2: Failed to create user");
	$last_id = mysqli_insert_id($con);
	echo "ID" . $last_id;
	
	$query = "INSERT INTO scores (user_id, score) VALUES ('$last_id', '0')";
	mysqli_query($con, $query) or die("3: Failed to create score");
?>