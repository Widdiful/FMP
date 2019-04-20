<?php
	$con = mysqli_connect('188.121.44.160', 'fmpscores', 'Password1!', 'fmpscores');

	// check for successful connection
	if (mysqli_connect_errno()){
		echo "1: Connection failed";
		exit();
	}
	
	$id = $_POST["id"];
	
	// get player info
	$query = "SELECT * FROM players LEFT JOIN scores ON players.id = scores.user_id WHERE id = '$id';";
	$result = mysqli_query($con, $query) or die("3: Failed to load player");
    if (mysqli_num_rows($result) > 0){
        while($row = mysqli_fetch_assoc($result)){
            echo 'id:' . $row["id"] . '|name:' . $row["name"] . '|avatar:' . $row["avatar"] . '|colour:' . $row["colour"] . '|score:' . $row["score"];
        }
    }
    else{    
        echo "2: Player not found";
    }
	
	// get player ranking
	$query = "SELECT id, name, avatar, colour, score, FIND_IN_SET(score, (SELECT GROUP_CONCAT(score ORDER BY score DESC) FROM players LEFT JOIN scores ON players.id = scores.user_id)) AS rank FROM players LEFT JOIN scores ON players.id = scores.user_id WHERE id = '$id'";
	$result = mysqli_query($con, $query) or die("4: Failed to load player ranking");
	if (mysqli_num_rows($result) > 0){
        while($row = mysqli_fetch_assoc($result)){
            echo '|rank:' . $row["rank"];
        }
    }
	
	echo ";";
?>