<?php
    $db_settings = parse_ini_file("../private/config.ini");

    $conn = new mysqli($db_settings['servername'], $db_settings['username'], $db_settings['password'], $db_settings['db_name']);
 
  // Check connection
    if($conn->connect_error)
    {
        die("ERROR: Could not connect. " . $conn->connect_error);
    }
 
  $sql = "SELECT player_name, score FROM score_board ORDER BY score DESC, date_created DESC";
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
    else 
    {
        echo "no records in the database";
    }

  
  $conn->close();
?>