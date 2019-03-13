-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 121.41.75.68    Database: qstore
-- ------------------------------------------------------
-- Server version	5.5.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tb_trade`
--

DROP TABLE IF EXISTS `tb_trade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_trade` (
  `id` int(6) NOT NULL AUTO_INCREMENT,
  `seller_nick` varchar(50) DEFAULT NULL,
  `byuer_nick` varchar(50) DEFAULT NULL,
  `title` varchar(100) DEFAULT NULL,
  `type` varchar(10) DEFAULT NULL COMMENT 'fixed 一口价',
  `created` datetime DEFAULT NULL,
  `iid` varchar(50) DEFAULT NULL,
  `price` varchar(10) DEFAULT NULL,
  `pic_path` varchar(200) DEFAULT NULL,
  `num` varchar(6) DEFAULT NULL,
  `tid` varchar(20) DEFAULT NULL,
  `buyer_message` varchar(200) DEFAULT NULL,
  `shipping_type` varchar(20) DEFAULT NULL,
  `alipay_no` varchar(30) DEFAULT NULL,
  `payment` varchar(10) DEFAULT NULL,
  `discount_fee` varchar(10) DEFAULT NULL,
  `adjust_fee` varchar(10) DEFAULT NULL,
  `receiver_name` varchar(30) DEFAULT NULL,
  `receiver_state` varchar(30) DEFAULT NULL,
  `receiver_city` varchar(50) DEFAULT NULL,
  `receiver_district` varchar(30) DEFAULT NULL,
  `receiver_address` varchar(100) DEFAULT NULL,
  `receiver_zip` varchar(10) DEFAULT NULL,
  `receiver_mobile` varchar(30) DEFAULT NULL,
  `receiver_phone` varchar(30) DEFAULT NULL,
  `num_iid` varchar(30) DEFAULT NULL,
  `regdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_trade`
--

LOCK TABLES `tb_trade` WRITE;
/*!40000 ALTER TABLE `tb_trade` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_trade` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-09-09 23:32:26
