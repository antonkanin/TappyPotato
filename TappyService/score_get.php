<?php
  $conn = new mysqli("localhost", "root", "", "tappy_potato");
 
  // Check connection
  if($conn->connect_error)
  {
      die("ERROR: Could not connect. " . $conn->connect_error);
  }
 
  $sql = "SELECT player, score FROM score_board ORDER BY score DESC, date DESC";
  $result = $conn->query($sql);
  
  if (!$result)
  {
	  trigger_error("Invalid query: " . $conn->error);
  }
  
  $resultArray = array();
  
  if ($result->num_rows > 0)
  {
	  while($row = $result->fetch_assoc())
	  {
		  $resultArray[] = $row;
	  }
	  echo json_encode($resultArray);
  }
  
  $conn->close();
?>