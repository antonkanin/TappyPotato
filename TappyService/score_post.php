<?php
	$conn = new mysqli("localhost", "root", "", "tappy_potato");
	
	if($conn->connect_error)
	{
		die("ERROR: Could not connect. " . $conn->connect_error);
	}
 
	
	$sql = "INSERT INTO score_board (player, score, date) VALUES ('".$_POST["name"]."','".$_POST["score"]."', now())";
	echo $sql;
	if($conn->query($sql) === TRUE)
	{
		echo "Records inserted successfully.";
	} 
	else
	{
		echo "ERROR: Could not able to execute $sql. " . $conn->error;
	}
 
	$conn->close();
?>
