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
-- Table structure for table `tb_prod_cost_modify_record`
--

DROP TABLE IF EXISTS `tb_prod_cost_modify_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_prod_cost_modify_record` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `p_id` int(11) NOT NULL,
  `p_name` varchar(200) NOT NULL,
  `p_code` varchar(50) NOT NULL,
  `old_cost` decimal(8,2) NOT NULL,
  `new_cost` decimal(8,2) NOT NULL,
  `staff_name` varchar(30) NOT NULL,
  `regdate` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_prod_cost_modify_record`
--

LOCK TABLES `tb_prod_cost_modify_record` WRITE;
/*!40000 ALTER TABLE `tb_prod_cost_modify_record` DISABLE KEYS */;
INSERT INTO `tb_prod_cost_modify_record` VALUES (2,1448,'SN-尚尼赠品三件套餐具(盒装)','SN-ZP006-SJT-X',0.00,99.00,'吕家燕','2016-07-02 14:01:23'),(3,1457,'SN-尚尼赠品西餐叉','SN-ZP006-MTSJT-HZ',0.00,48.00,'吕家燕','2016-07-02 14:01:41'),(4,1163,'尚尼简装赠品四件套西餐具','SN-ZP001-158',0.00,138.00,'吕家燕','2016-07-02 14:02:02'),(5,1341,'尚尼赠品密封罐-E2016-12*10CM','SN-ZP005-E2016',0.00,68.00,'吕家燕','2016-07-02 14:02:16'),(6,1301,'尚尼赠品旧金山茶匙','SN-ZP004-P080-202-1',0.00,48.00,'吕家燕','2016-07-02 14:02:27'),(7,1500,'尚尼赠品果盆-E2011-26CM','SN-ZP009-E2011',0.00,204.00,'吕家燕','2016-07-02 14:05:50'),(8,1499,'尚尼赠品果盆-E2010-24CM','SN-ZP008-E2010',0.00,188.00,'吕家燕','2016-07-02 14:06:02'),(9,1292,'尚尼赠品果篮-E2012-20CM','SN-ZP003-E2012',0.00,98.00,'吕家燕','2016-07-02 14:06:13'),(10,1501,'尚尼赠品果篮-E2013-24CM','SN-ZP010-E2013',0.00,198.00,'吕家燕','2016-07-02 14:06:30'),(11,1446,'尚尼赠品西餐刀','SN-ZP007-XCD',0.00,68.00,'吕家燕','2016-07-02 14:06:40'),(12,1291,'尚尼赠品密封罐-E2015-12*7CM','SN-ZP002-E2015',0.00,58.00,'吕家燕','2016-07-02 14:06:57'),(13,1374,'尚尼小号油壶胶圈','SN-PRM008-JQX',0.00,15.00,'吕家燕','2016-07-02 14:07:13'),(14,1563,'EKO-9217-静音红色-5L 4个/箱','EKO-lj035-9217-5L-H',0.00,105.00,'吕家燕','2016-07-02 14:08:16'),(15,1511,'SLR-32850-080-银点水果刀','SLR-32850-080-YD',0.00,89.00,'吕家燕','2016-07-02 14:10:11'),(16,1531,'SLR-40412-201-浅烧锅','slr-40412-201',0.00,368.00,'吕家燕','2016-07-02 14:12:29'),(17,1510,'SLR-32857-180-银点多用刀','SLR-32857-180-YD',0.00,128.00,'吕家燕','2016-07-02 14:13:15'),(18,1509,'SLR-32859-180-银点中片刀','SLR-32859-180-YD',0.00,298.00,'吕家燕','2016-07-02 14:13:51'),(19,1510,'SLR-32857-180-银点多用刀','SLR-32857-180-YD',128.00,228.00,'吕家燕','2016-07-02 14:14:14');
/*!40000 ALTER TABLE `tb_prod_cost_modify_record` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-09-09 23:38:50
